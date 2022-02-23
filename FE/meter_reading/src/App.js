import React, { useState } from "react";
import "./App.css";
import DataDisplay from "./DataDisplay";
import UploadData from "./UploadData";

const App = () => {
  const [readings, setReadings] = useState([]);
  const [displayTitle, setDisplayTitle] = useState(true);
  
  return (
    <div style={{textAlign: 'center'}}>
      <UploadData
      setReadings = {setReadings}
      setDisplayTitle = {setDisplayTitle}
      displayTitle = {displayTitle}
      >
        </UploadData>      
      <DataDisplay readings = {readings}></DataDisplay>
    </div>
  );
};

export default App;
