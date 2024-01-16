import React, { useState } from "react";
import { Form, Button, InputGroup } from "react-bootstrap";

const PatientSearch = ({ onSearch }) => {
  const [tcId, setTcId] = useState("");
  const [patientInfo, setPatientInfo] = useState(null);
  const [searchError, setSearchError] = useState(false);

  const handleSearch = async (e) => {
    e.preventDefault();
    setPatientInfo(null);
    setSearchError(false);
    try {
      const response = await fetch("https://run.mocky.io/v3/05798132-9471-4361-ae42-39f63506cccd");
      const data = await response.json();
      const patient = data.validTCNumbers.find((p) => p.tcNumber === tcId);

      if (patient) {
        setPatientInfo(patient.fullName);
        onSearch(tcId, patient.fullName);
      } else {
        setSearchError(true);
      }
    } catch (error) {
      console.error("Error fetching TC numbers:", error);
      setSearchError(true);
    }
  };

  return (
    <Form onSubmit={handleSearch}>
      <Form.Group>
        <Form.Label>TC Id no:</Form.Label>
        <InputGroup>
          <Form.Control
            type="text"
            placeholder="Enter TC ID"
            value={tcId}
            onChange={(e) => setTcId(e.target.value)}
            required
          />
          <Button variant="primary" type="submit">
            SEARCH
          </Button>
        </InputGroup>
      </Form.Group>
      {patientInfo && (
        <div className="mt-2">
          Full Name: <strong>{patientInfo}</strong>
        </div>
      )}
      {searchError && (
        <div className="text-danger mt-2">Invalid TC Number or Not Found</div>
      )}
    </Form>
  );
};

export default PatientSearch;
