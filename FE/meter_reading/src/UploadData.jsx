import React, {useState} from "react";
import { parse } from "papaparse";


const UploadData = ({setReadings, setDisplayTitle, displayTitle}) => {
  const [isHighlight, setIsHighlight] = useState(false);
  const [isValidResponse, setIsValidResponse] = useState(true);


    const uploadData = async (data) => {
        const fetchURL = "http://localhost:8000/update-meter-reading";
        const fetchOptions = {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
          body: JSON.stringify({
            data,
          }),
        };
        const response = await fetch(fetchURL, fetchOptions);
        if (response.status === 200) {
            const content = await response.json();
            setDisplayTitle(false)
            setReadings(content);
            setIsValidResponse(true);
        } else {
            setIsValidResponse(false);
            setIsHighlight(false);

        }
      };
    
  return (
    <div>
      <h1 style={{ display: displayTitle ? "block" : "none" }}>
        Meter Reading Drop Zone
      </h1>

      <h3 style={{ display: displayTitle ? "block" : "none" }}>
        Please drop your *.csv file on the screen
      </h3>
    <h3 style = {{display: isValidResponse? "none": "block", color: "red"}}>Please check the data uploaded, either the data in the file is invalid or the data is already uploaded in the database</h3>

      <div
        className="drop-div"
        style={{ display: displayTitle ? "block" : "none",
        background: isHighlight? "rgb(214 251 255": "#fff",
        boxShadow:  isHighlight? "0 0 0 rgb(0 0 0)":"0 0 30px rgb(0 0 0 / 0.6)"}}
        onDragEnter={() => {
            setIsHighlight(true);
          }}
          onDragLeave={() => {
            setIsHighlight(false);
          }}
        onDragOver={(e) => {
          e.preventDefault();
        }}
        onDrop={(e) => {
          e.preventDefault();

          Array.from(e.dataTransfer.files)
            .filter((file) => file.type === "application/vnd.ms-excel")
            .forEach(async (file) => {
              const text = await file.text();
              const result = parse(text);
              uploadData(result.data);
            });
        }}
      ></div>
    </div>
  );
};

export default UploadData;
