import React from 'react';
import { Card } from 'react-bootstrap';

const TotalPrice = ({ medicines }) => {
  const totalPrice = medicines.reduce((total, item) => total + Number(item.price), 0);

  return (
    <Card>
      <Card.Body>
        <Card.Text>Total: {totalPrice.toFixed(2)} TL</Card.Text>
      </Card.Body>
    </Card>
  );
};

export default TotalPrice;
