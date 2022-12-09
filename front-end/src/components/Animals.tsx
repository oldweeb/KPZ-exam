import { DatePicker, DefaultButton, PrimaryButton, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { IAnimal, IAnimalDto } from "../models";
import './Animals.scss';

const API_URL = 'https://localhost:7278/api';

const Animals: React.FunctionComponent = () => {
  const [animals, setAnimals] = useState<IAnimal[]>([]);
  const [selectedAnimalId, setSelectedAnimalId] = useState<string>('');
  const [selectedAnimal, setSelectedAnimal] = useState<IAnimal>();

  const getAnimals = async (): Promise<IAnimal[]> => {
    const response = await fetch(API_URL + '/animals/all');
    return await response.json();
  }

  useEffect(() => {
    getAnimals().then(animals => {
      setAnimals(animals);
    })
  }, []);

  useEffect(() => {
    setSelectedAnimal(animals.filter(animal => animal.id === selectedAnimalId)[0]);
  }, [selectedAnimalId]);

  const selectAnimalHandler = (id: string) => {
    setSelectedAnimalId(id);
  }

  const deleteHandler = () => {
    fetch(API_URL + `/animals/${selectedAnimalId}`, { method: 'DELETE' }).then(() => {
      getAnimals().then(animals => setAnimals(animals));
    })
  };

  const updateHandler = () => {
    const data: IAnimalDto = {
      animalType: selectedAnimal!.animalType.name,
      dateOfBirth: selectedAnimal!.dateOfBirth,
      name: selectedAnimal!.name,
      owner: selectedAnimal!.ownerName
    };

    fetch(API_URL + '/animals', {
      method: 'PUT',
      body: JSON.stringify({
        animalId: selectedAnimalId,
        animal: data
      }),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(() => {
      setSelectedAnimalId('');
      getAnimals().then(animals => setAnimals(animals));
    })
  };

  const nameChangeHandler = (event: any, newValue?: string) => {
    setSelectedAnimal({
      animalType: selectedAnimal!.animalType,
      dateOfBirth: selectedAnimal!.dateOfBirth,
      id: selectedAnimal!.id,
      ownerName: selectedAnimal!.ownerName,
      name: newValue ?? ''
    });
  };

  const ownerNameChangeHandler = (event: any, newValue?: string) => {
    setSelectedAnimal({
      animalType: selectedAnimal!.animalType,
      dateOfBirth: selectedAnimal!.dateOfBirth,
      id: selectedAnimal!.id,
      ownerName: newValue ?? '',
      name: selectedAnimal!.name
    });
  };

  const typeChangeHandler = (event: any, newValue?: string) => {
    setSelectedAnimal({
      animalType: {
        id: '',
        name: newValue ?? ''
      },
      dateOfBirth: selectedAnimal!.dateOfBirth,
      id: selectedAnimal!.id,
      ownerName: selectedAnimal!.ownerName,
      name: selectedAnimal!.name
    });
  }

  const dateChangeHandler = (date?: Date | null) => {
    setSelectedAnimal({
      animalType: selectedAnimal!.animalType,
      dateOfBirth: date!,
      id: selectedAnimal!.id,
      ownerName: selectedAnimal!.ownerName,
      name: selectedAnimal!.name
    });
  };

  return (
    <div className="container">
      <table>
        <thead>
          <tr>
            <td>Id</td>
            <td>Name</td>
            <td>Date Of Birth</td>
            <td>Owner</td>
            <td>Type</td>
          </tr>
        </thead>
        <tbody>
          {animals.map(animal => {
            return (
              <tr key={animal.id}>
                <td><a href='#' onClick={() => selectAnimalHandler(animal.id)}>{animal.id}</a></td>
                <td>{animal.name}</td>
                <td>{animal.dateOfBirth}</td>
                <td>{animal.ownerName}</td>
                <td>{animal.animalType.name}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
      {selectedAnimalId !== '' && (
        <div className="form">
          <TextField onChange={nameChangeHandler} label='Name' value={selectedAnimal?.name} />
          <DatePicker onSelectDate={dateChangeHandler} formatDate={(date?: Date) => date?.toString() ?? ''} label='Date Of Birth' value={selectedAnimal?.dateOfBirth} />
          <TextField onChange={ownerNameChangeHandler} label='Owner' value={selectedAnimal?.ownerName} />
          <TextField onChange={typeChangeHandler} label='Type' value={selectedAnimal?.animalType.name} />
          <PrimaryButton text='Update' onClick={updateHandler} />
          <DefaultButton text='Delete' onClick={deleteHandler} />
        </div>
      )}
    </div>

  );
}

export default Animals;