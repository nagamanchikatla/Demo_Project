import React from 'react';
import Table from "./table";

const DataDisplay = ({readings}) => {
    return (
        <div>
            <div className="valid-table" style={{display: (readings.filter((reading) => reading[3] === "isValid").length) ? 'block':'none'}}>
      <h2 style={{textAlign: 'center'}}>Total Files processed {readings.length-1}</h2>
          <h4>Valid Data uploaded to DB</h4>
          <Table
            readings={readings.filter((reading) => reading[3] === "isValid")}
          ></Table>
          <h6>Count is {readings.filter((reading) => reading[3] === "isValid").length}</h6>
        </div>
        <div style={{display: (readings.filter((reading) => reading[3] === "isInvalid").length) ? 'block':'none'}}>
          <h4>Invalid Data which is not uploaded to DB</h4>
          <Table
            readings={readings.filter((reading) => reading[3] === "isInvalid")}
          ></Table>
          <h6>Count is {readings.filter((reading) => reading[3] === "isInvalid").length}</h6>
        </div>
        <div style={{display: (readings.filter((reading) => reading[3] === "isOlderValue").length) ? 'block':'none'}}>
          <h4>Older Value Data which is not uploaded to DB</h4>
          <Table
            readings={readings.filter((reading) => reading[3] === "isOlderValue")}
          ></Table>
          <h6>Count is {readings.filter((reading) => reading[3] === "isOlderValue").length}</h6>
        </div>
        <div  style={{display: (readings.filter((reading) => reading[3] === "isDuplicate").length) ? 'block':'none'}}>
          <h4>Duplicate Data which is not uploaded to DB</h4>
          <Table
            readings={readings.filter((reading) => reading[3] === "isDuplicate")}
          ></Table>
          <h6>Count is {readings.filter((reading) => reading[3] === "isDuplicate").length}</h6>
        </div>
        <div  style={{display: (readings.filter((reading) => reading[3] === "isInvalidAccountId").length) ? 'block':'none'}}>
          <h4>Invalid Account Id Data which is not uploaded to DB</h4>
          <Table
            readings={readings.filter((reading) => reading[3] === "isInvalidAccountId")}
          ></Table>
          <h6>Count is {readings.filter((reading) => reading[3] === "isInvalidAccountId").length}</h6>
        </div>
      </div>
    );
};

export default DataDisplay;