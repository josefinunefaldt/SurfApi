function GetWaves( name) {
    fetch(`http://localhost:5130/Surf?placeName=${name}`)
    .then((response) => response.json())
  .then((data) => {
    const result = document.getElementById("result");

    data.forEach((element) => {
      const item = document.createElement("div");
      item.textContent = element.hours
      result.appendChild(item);
    });
  })
  .catch((error) => {
    console.error('Error:', error);
  });
  }