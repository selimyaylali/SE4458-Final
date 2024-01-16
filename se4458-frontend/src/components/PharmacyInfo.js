import React from 'react';
import { Card } from 'react-bootstrap';

const PharmacyInfo = ({ pharmacyName }) => {
  return (
    <Card className="mb-4">
      <Card.Body>
        <Card.Title>{pharmacyName}</Card.Title>
      </Card.Body>
    </Card>
  );
};

export default PharmacyInfo;
