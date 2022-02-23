const Table = ({readings}) => {
    return (
        <table>
           <thead>
             <tr>
             <th>AccountId</th>
             <th>MeterReadingDateTime</th>
             <th>MeterReadValue</th>
             </tr>
           </thead>
           <tbody>
             {readings.map((mReading) =>
             <tr key={mReading[0]}>
               <td key={mReading[0]}>{mReading[0]}</td>
               <td>{mReading[1]}</td>
               <td>{mReading[2]}</td>
             </tr>             
             )}
           </tbody>
         </table>        
    );
}

export default Table;