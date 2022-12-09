import { DatePicker, DayOfWeek, defaultDatePickerStrings, Dropdown, IDropdownOption, PrimaryButton, TextField } from "@fluentui/react";
import { FormEvent, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IAnimal, IVisitDto } from "../models";

const API_URL = 'https://localhost:7278';

const AddVisit: React.FunctionComponent = () => {
  const [date, setDate] = useState<Date | null | undefined>();
  const [diagnosis, setDiagnosis] = useState<string>('');
  const [animalId, setAnimalId] = useState<string>('');
  const [animals, setAnimals] = useState<IAnimal[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetch(API_URL + '/api/animals/all')
      .then(result => result.json())
      .then((animals: IAnimal[]) => {
        setAnimals(animals);
      });
  }, []);

  const animalChangeHandler = (e: any, option?: IDropdownOption) => {
    setAnimalId(option?.key as string ?? '');
  }

  const diagnosisChangeHandler = (event: any, newValue?: string) => {
    setDiagnosis(newValue ?? '');
  }

  const dateChangeHandler = (date?: Date | null) => {
    setDate(date);
  };

  const submitHandler = (e?: FormEvent<HTMLFormElement>) => {
    e?.preventDefault();

    if (!date) {
      alert('Date is required field');
      return;
    }

    const visit: IVisitDto = {
      dateOfVisit: date!,
      diagnosis: diagnosis,
      animalId: animalId
    };

    fetch(API_URL + '/api/visits', {
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'POST',
      body: JSON.stringify(visit)
    }).then(() => {
      alert('Successfully added');
      navigate('/');
    }).catch((error) => {
      alert('Something went wrong.');
      console.error(error);
    })
  };

  const options: IDropdownOption[] = animals.map(a => {
    return {
      key: a.id,
      text: [a.name, a.animalType.name, a.ownerName].filter(p => !!p).join(', ')
    };
  });

  return (
    <form className='form' onSubmit={submitHandler}>
      <Dropdown
        selectedKey={animalId || undefined}
        label='Animal'
        onChange={animalChangeHandler}
        options={options}
      />
      <DatePicker
        firstDayOfWeek={DayOfWeek.Monday}
        isRequired={true}
        placeholder='Select a date...'
        strings={defaultDatePickerStrings}
        onSelectDate={dateChangeHandler}
        value={date!}
      />
      <TextField
        label='Diagnosis'
        type='text'
        required
        value={diagnosis}
        onChange={diagnosisChangeHandler}
      />
      <PrimaryButton text='Add' type='submit' />
    </form>
  );
};

export default AddVisit;