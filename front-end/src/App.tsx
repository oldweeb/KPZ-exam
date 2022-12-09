import { registerIcons, Stack } from '@fluentui/react';
import { CalendarIcon, DownIcon, UpIcon } from '@fluentui/react-icons-mdl2';
import { PrimaryButton } from '@fluentui/react/lib/Button';
import React from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import './App.scss';


registerIcons({
  icons: {
    calendar: <CalendarIcon />,
    up: <UpIcon />,
    down: <DownIcon />
  }
})

const App: React.FunctionComponent = () => {
  const navigate = useNavigate();

  return (
    <Stack horizontal={false} className='menu'>
      <PrimaryButton text='Add Animal' onClick={() => navigate('add-animal')} />
      <PrimaryButton text='Add Visit' onClick={() => navigate('add-visit')} />
      <PrimaryButton text='Animals' onClick={() => navigate('animals')} />
      <PrimaryButton text='Visits' onClick={() => navigate('visits')} />
    </Stack>
  );
};

export default App;
