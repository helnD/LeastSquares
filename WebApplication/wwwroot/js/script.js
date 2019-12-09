var pointNumber = 1;
var numberOfParams = 1;
var funcIsCalculated = false;

var height = 350;
var width = 700;
var margin = 30;
var xAxisLength = width - 2 * margin;
var yAxisLength = height - 2 * margin;

$("#number_of_params").focus(function(){
    this.select();
});

draw();

function draw() {
    rawData = [
        {x: 10, y: 67}, {x: 20, y: 74}, {x: 30, y: 63},
        {x: 40, y: 56}, {x: 50, y: 24}, {x: 60, y: 26},
        {x: 70, y: 19}, {x: 80, y: 42}, {x: 90, y: 88},
        {x: 2.2, y: 94.5}
    ],
    data=[];

    // создание объекта svg
    var svg = d3.select("svg");

    // длина оси X = ширина контейнера svg - отступ слева и справа


    // длина оси Y = высота контейнера svg - отступ сверху и снизу


    // функция интерполяции значений на ось Х
    var scaleX = d3.scale.linear()
        .domain([0, 200])
        .range([0, xAxisLength]);

    // функция интерполяции значений на ось Y
    var scaleY = d3.scale.linear()
        .domain([100, 0])
        .range([0, yAxisLength]);

    // масштабирование реальных данных в данные для нашей координатной системы
    for(i = 0; i < rawData.length; i++)
        data.push({
            x: scaleX(rawData[i].x) + margin,
            y: scaleY(rawData[i].y) + margin});

    // создаем ось X
    var xAxis = d3.svg.axis()
        .scale(scaleX)
        .orient("bottom")
        .ticks(20);

    // создаем ось Y
    var yAxis = d3.svg.axis()
        .scale(scaleY)
        .orient("left");

    // отрисовка оси Х
    svg.append("g")
        .attr("class", "x-axis")
        .attr("transform",  // сдвиг оси вниз и вправо
            "translate(" + margin + "," + (height - margin) + ")")
        .call(xAxis);

    // отрисовка оси Y
    svg.append("g")
        .attr("class", "y-axis")
        .attr("transform", // сдвиг оси вниз и вправо на margin
                "translate(" + (margin) + "," + margin + ")")
        .call(yAxis);

    // создаем набор вертикальных линий для сетки
    d3.selectAll("g.x-axis g.tick")
        .append("line")
        .classed("grid-line", true)
        .attr("x1", 0)
        .attr("y1", 0)
        .attr("x2", 0)
        .attr("y2", - (yAxisLength));

    // рисуем горизонтальные линии координатной сетки
    d3.selectAll("g.y-axis g.tick")
        .append("line")
        .classed("grid-line", true)
        .attr("x1", 0)
        .attr("y1", 0)
        .attr("x2", xAxisLength)
        .attr("y2", 0);

    // функция, создающая по массиву точек линии
    var line = d3.svg.line()
        .x(function(d){return d.x;})
        .y(function(d){return d.y;});

    // добавляем путь
    /*svg.append("g").append("path")
        .attr("d", line(data))
        .style("stroke", "green")
        .style("stroke-width", 1);*/

    svg.on("click", function() {
        var mouse = d3.mouse(this);
        let x = Math.round((mouse[0] - margin) * 200 / xAxisLength * 10000) / 10000;
        let y = Math.round((height - mouse[1] - margin) * 100 / yAxisLength * 10000) / 10000;
        let color = getRandomColor();
        svg
            .append("circle")
            .attr("class", "dot")
            .attr("id", "dot-" + pointNumber)
            .attr("cx", mouse[0])
            .attr("cy", mouse[1])
            .attr("fill", color)
            .attr("stroke-width", "1px")
            .attr("r", 5)
            .on("mouseover", function() {
                    console.log(x, y);
                })
            .on("mouseout", function() { });
        addPointToList(x, y, color);
    })
}

function sendForm() {
    var request = new XMLHttpRequest();

    let coordinates = document.getElementsByClassName("coordinate");
    let coordStr = "";

    for (let i = 0; i < coordinates.length; i++) {
        var start = coordinates[i].innerText.indexOf(" ") + 1;
        var end = coordinates[i].innerText.indexOf(",");
        coordStr += `&x${i}=${coordinates[i].innerText.slice(1, end)}`.replace(".", ",");
        coordStr += `&y${i}=${coordinates[i].innerText.slice(start, this.length - 1)}`.replace(".", ",");
    }

    var string =
        "count=" + coordinates.length
        + coordStr
        + "&parameters=" + document.getElementById("number_of_params").value;

    console.log(string);

    request.open("GET", "https://localhost:5001/Home/Calculate?" + string, false);
    request.onreadystatechange = function() {
        if (request.readyState === 4 && request.status === 200) {
            let data = JSON.parse(request.response);
            let dots = data.relativeView.dots;
            let params = data.analyticalView.parameters;
            var svg = d3.select("svg");
            let funcData = [];

            if (document.getElementsByClassName("graph").length > 0)
              document.getElementsByClassName("graph")[0].remove();

            for (let i = 0; i < dots.length; i++) {
                funcData.push({
                  x: (dots[i].x * xAxisLength / 200) + margin,
                  y: height - (dots[i].y * yAxisLength / 100) - margin
                });
            }
            var line = d3.svg.line()
                .x(function(d){return d.x;})
                .y(function(d){return d.y;});
            svg.append("g")
                .append("path")
                .attr("d", line(funcData))
                .attr("class", "graph")
                .style("stroke", "#000000")
                .style("stroke-width", 3);

            showValues(params);
        }
    }
    request.send();
}

function addPointToList(x, y, color) {
    let points = document.getElementById("points_list");

    let point = document.createElement("div");
    let clr = document.createElement("span");
    let coord = document.createElement("span");
    let btn = document.createElement("input");

    clr.className = "color";
    clr.style = "background-color: " + color;

    coord.className = "coordinate";
    coord.innerText = "(" + x + ", " + y + ")";

    btn.className = "del-dot-btn btn";
    btn.id = "btn-" + pointNumber;
    btn.type = "button";
    btn.setAttribute('onclick', "deleteDot(this.id)");

    point.classList.add("point");
    point.id = "point-" + pointNumber;
    point.setAttribute('onmouseover', "highlightPoint(this.id)");
    point.setAttribute('onmouseout', "cancelHighlight(this.id)")

    points.append(point);
    point.append(clr, coord, btn);

    pointNumber++;
}

function highlightPoint(id) {
    let num = id.slice(id.indexOf("-") + 1);
    let dot = document.getElementById("dot-" + num);
    dot.setAttribute('r', 8);
}

function cancelHighlight(id) {
    let num = id.slice(id.indexOf("-") + 1);
    let dot = document.getElementById("dot-" + num);
    dot.setAttribute('r', 5);
}

function clearAll() {
    let points = document.getElementById("points_list").children;
    let dots = document.getElementsByClassName("dot");

    for (let i = points.length - 1; i >= 0; i--) {
        points[i].remove();
        dots[i].remove();
    }

    document.getElementsByClassName("graph")[0].remove();
    clearFunction();

    pointNumber = 1;
}

function deleteDot(id) {
    let num = id.slice(id.indexOf("-") + 1);
    document.getElementById("point-" + num).remove();
    document.getElementById("dot-" + num).remove();
}

function getRandomColor() {
    let red = Math.round(Math.random() * 255);
    let green = Math.round(Math.random() * 255);
    let blue = Math.round(Math.random() * 255);
    return "rgb(" + red + ", " + green + ", " + blue + ")";
}

function changeFunc(val) {
    if (val == "" || val < 1)
        document.getElementById("number_of_params").value
            = numberOfParams;
    if (funcIsCalculated == true) clearFunction();
    if (val > numberOfParams) {
        let count = val - numberOfParams;
        for (let i = 0; i < count; i++)
            addParam();
    }
    if (val < numberOfParams) {
        let count = numberOfParams - val;
        for (let i = 0; i < count; i++)
            deleteParam(val);
    }
}

function addParam() {
    let func = document.getElementsByClassName("func-items")[0];

    let newItem = document.createElement("div");
    let plus = document.createElement("div");
    let newValue = document.createElement("div");
    let xSpan = document.createElement("div");
    let valSpan = document.createElement("div");
    let a = document.createElement("div");
    let sub = document.createElement("sub");
    let x = document.createElement("div");
    let sup = document.createElement("sup");

    newItem.classList.add("item");
    newItem.classList.add("item-" + numberOfParams);
    newValue.classList.add("param");
    plus.innerHTML = "&#160;+&#160;";
    a.innerText = "a";
    sub.innerText =`${numberOfParams + 1}`;
    x.innerText = "x";
    sup.innerText = numberOfParams == 1 ? `` : `${numberOfParams}`;
    xSpan.classList.add("x");
    valSpan.id = "val-" + (numberOfParams + 1);
    valSpan.classList.add("value");
    xSpan.append(x, sup);
    newValue.append(a, sub, valSpan);
    newItem.append(plus, newValue, xSpan);
    func.append(newItem);

    numberOfParams++;
}

function deleteParam(val) {
    let items = document.getElementsByClassName("func-items")[0].children;
    let index = items.length - 1;

    if (val > 0) {
        items[index].remove();
        numberOfParams--;
    }
}

function showValues(values) {
    let params = document.getElementsByClassName("param");
    let valDivs = document.getElementsByClassName("value");

    for (let i = 0; i < params.length; i++) {
        params[i].style = "color:transparent;transform:translate(0px,-20px);";
        valDivs[i].innerText = values[i];
        valDivs[i].style = "color:#8d093c;transform:translate(5px,20px);";
    }

    funcIsCalculated = true;
}

function clearFunction() {
    let params = document.getElementsByClassName("param");
    let valDivs = document.getElementsByClassName("value");

    for (let i = 0; i < params.length; i++) {
        params[i].style = "";
        valDivs[i].innerText = "";
        valDivs[i].style = "";
    }

    funcIsCalculated = false;
}
