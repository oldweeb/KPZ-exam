export interface IAnimalDto {
  name: string;
  dateOfBirth: Date;
  owner: string;
  animalType: string;
}

export interface IVisitDto {
  animalId: string;
  dateOfVisit: Date;
  diagnosis: string;
}

export interface IAnimal {
  id: string;
  name: string,
  animalType: IAnimalType;
  ownerName: string;
  dateOfBirth: Date;
}

export interface IAnimalType {
  id: string;
  name: string;
}

export interface IVisit {
  id: string;
  animal: IAnimal;
  dateOfVisit: Date;
  diagnosis: string;
}