import React, { useState } from "react";
import PharmacyInfo from "./components/PharmacyInfo";
import PatientSearch from "./components/PatientSearch";
import MedicineList from "./components/MedicineList";
import TotalPrice from "./components/TotalPrice";
import "bootstrap/dist/css/bootstrap.min.css";

const App = () => {
  const handleSearch = (tcId) => {
    // You would typically make an API call here to search for the patient
    console.log("Searching for patient with TC ID:", tcId);
  };

  const [medicines, setMedicines] = useState([
    { name: "Norodol", price: 20 },
    { name: "Voltaren", price: 50 },
  ]);

  const addMedicine = (name, price) => {
    setMedicines([...medicines, { name, price: parseFloat(price) }]);
  };

  const editMedicine = (index) => {
    // Placeholder for editing logic
    console.log("Editing medicine at index:", index);
  };

  const deleteMedicine = (index) => {
    const updatedMedicines = medicines.filter((_, idx) => idx !== index);
    setMedicines(updatedMedicines);
  };

  return (
    <div className="container mt-3">
      <PharmacyInfo pharmacyName="Eczane Yaylalı" />
      <PatientSearch onSearch={handleSearch} />
      <MedicineList
        medicines={medicines}
        onAdd={addMedicine}
        onEdit={editMedicine}
        onDelete={deleteMedicine}
      />
      <TotalPrice medicines={medicines} />
    </div>
  );
};

export default App;
