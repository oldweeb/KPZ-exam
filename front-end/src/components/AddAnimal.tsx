import { FormEvent, FormEventHandler, useState } from 'react';
import { TextField, DatePicker, DayOfWeek, defaultDatePickerStrings, PrimaryButton } from '@fluentui/react';
import './AddAnimal.scss';
import { IAnimalDto } from '../models';
import { useNavigate } from 'react-router-dom';

const API_URL = 'https://localhost:7278';

const AddAnimal: React.FunctionComponent = () => {
  const [date, setDate] = useState<Date | null | undefined>();
  const [name, setName] = useState<string>('');
  const [ownerName, setOwnerName] = useState<string>('');
  const [type, setType] = useState<string>('');
  const navigate = useNavigate();

  const nameChangeHandler = (event: any, newValue?: string) => {
    setName(newValue ?? '');
  };

  const ownerNameChangeHandler = (event: any, newValue?: string) => {
    setOwnerName(newValue ?? '');
  };

  const typeChangeHandler = (event: any, newValue?: string) => {
    setType(newValue ?? '');
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

    const animal: IAnimalDto = {
      name,
      dateOfBirth: date!,
      owner: ownerName,
      animalType: type
    };

    fetch(API_URL + '/api/animals', {
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'POST',
      body: JSON.stringify(animal)
    }).then(() => {
      alert('Successfully added');
      navigate('/');
    }).catch((error) => {
      alert('Something went wrong.');
      console.error(error);
    })
  };

  return (
    <form className='form' onSubmit={submitHandler}>
      <TextField
        label='Name'
        type='text'
        value={name}
        onChange={nameChangeHandler}
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
        label='Owner Name'
        type='text'
        required
        value={ownerName}
        onChange={ownerNameChangeHandler}
      />
      <TextField
        label='Type'
        type='text'
        required
        value={type}
        onChange={typeChangeHandler}
      />
      <PrimaryButton text='Add' type='submit' />
    </form>
  );
};

export default AddAnimal;