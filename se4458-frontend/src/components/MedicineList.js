import React, { useState } from 'react';
import { Table, Button, Form, InputGroup, FormControl } from 'react-bootstrap';

const MedicineList = ({ medicines, onAdd, onEdit, onDelete }) => {
  const [newMedicine, setNewMedicine] = useState('');
  const [newPrice, setNewPrice] = useState('');

  const handleSubmit = (event) => {
    event.preventDefault();
    onAdd(newMedicine, newPrice);
    setNewMedicine('');
    setNewPrice('');
  };

  return (
    <>
      <Form onSubmit={handleSubmit}>
        <InputGroup className="mb-3">
          <FormControl
            placeholder="Medicine"
            value={newMedicine}
            onChange={(e) => setNewMedicine(e.target.value)}
          />
          <FormControl
            placeholder="Price"
            type="number"
            value={newPrice}
            onChange={(e) => setNewPrice(e.target.value)}
          />
          <Button variant="outline-secondary" type="submit">
            ADD
          </Button>
        </InputGroup>
      </Form>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {medicines.map((medicine, index) => (
            <tr key={index}>
              <td>{medicine.name}</td>
              <td>{medicine.price}</td>
              <td>
                <Button variant="info" onClick={() => onEdit(index)}>
                  EDIT
                </Button>{' '}
                <Button variant="danger" onClick={() => onDelete(index)}>
                  DELETE
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
};

export default MedicineList;
