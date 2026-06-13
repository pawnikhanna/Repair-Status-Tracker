import { useEffect, useState } from 'react';
import { RepairStatus, RepairStatusLabels } from './types';
import type { RepairJob } from './types';

const API_BASE_URL = 'http://localhost:5090/api/repairjobs'; 

function App() {
  const [jobs, setJobs] = useState<RepairJob[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchJobs = async () => {
    try {
      const response = await fetch(API_BASE_URL);
      if (!response.ok) throw new Error('Failed to retrieve vehicle files.');
      const data = await response.json();
      setJobs(data);
    } catch (err: any) {
      setError(err.message || 'An error occurred while connecting to the backend application.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    document.body.style.backgroundColor = '#ffffff';
    document.body.style.margin = '0';
    document.body.style.padding = '0';
    document.body.style.color = '#000000';
    
    fetchJobs();
  }, []);

  const handleStatusChange = async (id: string, newStatus: RepairStatus) => {
    try {
      const response = await fetch(`${API_BASE_URL}/${id}/status`, {
        method: 'PATCH',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ status: newStatus }),
      });

      if (!response.ok) throw new Error('Failed to update tracking assignment state rules.');
      
      setJobs(prevJobs =>
        prevJobs.map(job =>
          job.id === id ? { ...job, status: newStatus, lastUpdated: new Date().toISOString() } : job
        )
      );
    } catch (err: any) {
      alert(err.message || 'Error executing field modification request.');
    }
  };

  if (loading) return <div style={{ padding: '20px', fontFamily: 'Arial, sans-serif', backgroundColor: '#ffffff', color: '#000000' }}>Loading Repair Status Tracker...</div>;
  if (error) return <div style={{ padding: '20px', color: '#ff0000', fontFamily: 'Arial, sans-serif', backgroundColor: '#ffffff' }}>Error: {error}</div>;

  return (
    <div style={{ 
      backgroundColor: '#ffffff', 
      minHeight: '100vh', 
      width: '100%', 
      boxSizing: 'border-box',
      padding: '30px', 
      fontFamily: 'Arial, sans-serif',
      color: '#000000'
    }}>
      <header style={{ marginBottom: '25px' }}>
        <h1 style={{ margin: '0 0 5px 0', fontSize: '28px', fontWeight: 'bold', color: '#000000'}}>
          Crash Champions — Repair Status Tracker
        </h1>
      </header>

      <table style={{ 
        width: '100%', 
        borderCollapse: 'collapse', 
        border: '2px solid #000000',
        backgroundColor: '#ffffff'
      }}>
        <thead>
          <tr style={{ backgroundColor: '#ffffff', borderBottom: '2px solid #000000' }}>
            <th style={{ padding: '12px', border: '1px solid #000000', textAlign: 'left', fontWeight: 'bold' }}>Customer Name</th>
            <th style={{ padding: '12px', border: '1px solid #000000', textAlign: 'left', fontWeight: 'bold' }}>Vehicle Info</th>
            <th style={{ padding: '12px', border: '1px solid #000000', textAlign: 'left', fontWeight: 'bold' }}>Repair Location</th>
            <th style={{ padding: '12px', border: '1px solid #000000', textAlign: 'left', fontWeight: 'bold' }}>Current Status</th>
            <th style={{ padding: '12px', border: '1px solid #000000', textAlign: 'left', fontWeight: 'bold' }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {jobs.map(job => (
            <tr key={job.id} style={{ borderBottom: '1px solid #000000' }}>
              <td style={{ padding: '12px', border: '1px solid #000000', fontWeight: 'bold' }}>{job.customerName}</td>
              <td style={{ padding: '12px', border: '1px solid #000000' }}>{job.vehicleDetails}</td>
              <td style={{ padding: '12px', border: '1px solid #000000' }}>{job.repairCenter}</td>
              <td style={{ padding: '12px', border: '1px solid #000000' }}>
                {job.status === RepairStatus.Completed ? (
                  <span style={{
                    padding: '4px 10px',
                    borderRadius: '4px',
                    fontSize: '12px',
                    fontWeight: 'bold',
                    backgroundColor: '#28a745',
                    color: '#ffffff',
                    display: 'inline-block'
                  }}>
                    {RepairStatusLabels[job.status]}
                  </span>
                ) : (
                  <span style={{ fontSize: '14px', fontWeight: 'normal' }}>
                    {RepairStatusLabels[job.status]}
                  </span>
                )}
              </td>
              <td style={{ padding: '12px', border: '1px solid #000000' }}>
                <select
                  value={job.status}
                  onChange={(e) => handleStatusChange(job.id, Number(e.target.value) as RepairStatus)}
                  style={{ 
                    padding: '6px', 
                    border: '1px solid #000000', 
                    backgroundColor: '#ffffff',
                    color: '#000000',
                    cursor: 'pointer'
                  }}
                >
                  {Object.keys(RepairStatusLabels).map(key => (
                    <option key={key} value={key}>
                      {RepairStatusLabels[Number(key) as RepairStatus]}
                    </option>
                  ))}
                </select>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;