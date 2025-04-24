const updateBtn = document.querySelector("#updateBtn");
const input = document.querySelector("#dataInput");
const errorMsg = document.querySelector("#errorMsg");
const chartContainer = d3.select("#chart");

const COLORS = ["#4e79a7", "#f28e2c", "#e15759", "#76b7b2", "#59a14f"];
const width = 500;
const height = 300;
const margin = { top: 20, right: 20, bottom: 30, left: 40 };

updateBtn.addEventListener("click", () => {
  errorMsg.textContent = "";
  let values = input.value.split(",").map((v) => v.trim());

  if (!values.every((v) => /^-?\d+$/.test(v))) {
    errorMsg.textContent = "Ingrese solo números enteros separados por coma.";
    return;
  }

  values = values.map(Number);
  drawChart(values);
});

function drawChart(data) {
  chartContainer.selectAll("*").remove();

  const svg = chartContainer
    .append("svg")
    .attr("width", width)
    .attr("height", height);

  const x = d3
    .scaleBand()
    .domain(data.map((_, i) => i))
    .range([margin.left, width - margin.right])
    .padding(0.1);

  const y = d3
    .scaleLinear()
    .domain([0, d3.max(data)])
    .nice()
    .range([height - margin.bottom, margin.top]);

  svg
    .append("g")
    .attr("transform", `translate(0,${height - margin.bottom})`)
    .call(
      d3
        .axisBottom(x)
        .tickFormat((i) => i + 1)
        .tickSizeOuter(0)
    );

  svg
    .append("g")
    .attr("transform", `translate(${margin.left},0)`)
    .call(d3.axisLeft(y));

  svg
    .selectAll(".bar")
    .data(data)
    .enter()
    .append("rect")
    .attr("class", "bar")
    .attr("x", (_, i) => x(i))
    .attr("y", (d) => y(d))
    .attr("width", x.bandwidth())
    .attr("height", (d) => y(0) - y(d))
    .attr("fill", (d, i) => COLORS[i % COLORS.length])
    .each(function (_, i, nodes) {
      const current = d3.select(this);
      if (i > 0) {
        const prevColor = d3.select(nodes[i - 1]).attr("fill");
        if (current.attr("fill") === prevColor) {
          const newColor = COLORS.find((c) => c !== prevColor);
          current.attr("fill", newColor);
        }
      }
    });
}
