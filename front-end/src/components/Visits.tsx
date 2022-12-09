import { DatePicker, DefaultButton, Dropdown, IDropdownOption, PrimaryButton, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { IAnimal, IVisit, IVisitDto } from "../models";

const API_URL = 'https://localhost:7278/api';

const Visits: React.FunctionComponent = () => {
  const [visits, setVisits] = useState<IVisit[]>([]);
  const [animals, setAnimals] = useState<IAnimal[]>([]);
  const [selectedVisit, setSelectedVisit] = useState<IVisit>();
  const [selectedVisitId, setSelectedVisitId] = useState<string>('');

  console.log(selectedVisit);

  const getVisits = async (): Promise<IVisit[]> => {
    const response = await fetch(API_URL + '/visits/all');
    return await response.json();
  };

  const getAnimals = async (): Promise<IAnimal[]> => {
    const response = await fetch(API_URL + '/animals/all');
    return await response.json();
  }

  useEffect(() => {
    getVisits().then(visits => {
      setVisits(visits);
    });
  }, []);

  useEffect(() => {
    getAnimals().then(animals => {
      setAnimals(animals);
    });
  }, []);

  useEffect(() => {
    setSelectedVisit(visits.filter(visit => visit.id === selectedVisitId)[0]);
  }, [selectedVisitId]);

  const selectVisitHandler = (id: string) => {
    setSelectedVisitId(id);
  };

  const deleteHandler = () => {
    fetch(API_URL + `/visits/${selectedVisitId}`, { method: 'DELETE' }).then(() => {
      getVisits().then(visits => setVisits(visits));
    });
  };

  const updateHandler = () => {
    const data: IVisitDto = {
      animalId: selectedVisit!.animal.id,
      dateOfVisit: selectedVisit!.dateOfVisit,
      diagnosis: selectedVisit!.diagnosis
    };

    fetch(API_URL + '/visits', {
      method: 'PUT',
      body: JSON.stringify({
        visitId: selectedVisitId,
        visit: data
      }),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(() => {
      setSelectedVisitId('');
      getVisits().then(visits => setVisits(visits));
    })
  };

  const options: IDropdownOption[] = animals.map(a => {
    return {
      key: a.id,
      text: [a.name, a.animalType.name, a.ownerName].filter(p => !!p).join(', ')
    };
  });

  const animalChangeHandler = (e: any, option?: IDropdownOption) => {
    const animal = animals.filter(an => an.id === option!.key)[0];
    setSelectedVisit({
      animal: animal,
      dateOfVisit: selectedVisit!.dateOfVisit,
      diagnosis: selectedVisit!.diagnosis,
      id: selectedVisit!.id
    })
  }

  const diagnosisChangeHandler = (event: any, newValue?: string) => {
    setSelectedVisit({
      animal: selectedVisit!.animal,
      dateOfVisit: selectedVisit!.dateOfVisit,
      diagnosis: newValue ?? '',
      id: selectedVisit!.id
    })
  }

  const dateChangeHandler = (date?: Date | null) => {
    setSelectedVisit({
      animal: selectedVisit!.animal,
      dateOfVisit: date!,
      diagnosis: selectedVisit!.diagnosis,
      id: selectedVisit!.id
    })
  };

  return (
    <div className='container'>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Animal</th>
            <th>Date Of Visit</th>
            <th>Diagnosis</th>
          </tr>
        </thead>
        <tbody>
          {visits.map(visit => {
            return (
              <tr key={visit.id}>
                <td><a href='#' onClick={() => selectVisitHandler(visit.id)} >{visit.id}</a></td>
                <td>{[[visit.animal.name, visit.animal.animalType.name, visit.animal.ownerName].filter(p => !!p).join(', ')]}</td>
                <td>{visit.dateOfVisit}</td>
                <td>{visit.diagnosis}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
      {selectedVisitId !== '' && (
        <div className='form'>
          <Dropdown selectedKey={selectedVisit?.animal.id} label='Animal' onChange={animalChangeHandler} options={options} />
          <DatePicker formatDate={(date?: Date) => date?.toString() ?? ''} value={selectedVisit?.dateOfVisit} label='Date Of Visit' onSelectDate={dateChangeHandler} />
          <TextField value={selectedVisit?.diagnosis} label='Diagnosis' onChange={diagnosisChangeHandler} />
          <PrimaryButton text='Update' onClick={updateHandler} />
          <DefaultButton text='Delete' onClick={deleteHandler} />
        </div>
      )}
    </div>
  );
};

export default Visits;