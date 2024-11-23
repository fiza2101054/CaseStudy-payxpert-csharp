--Creating the database
create database payxpert

--Creating the Employee table 
create table Employee( 
EmployeeId int primary key identity(1,1),
FirstName varchar(50) not null,
LastName varchar(50) not null,
DateOfBirth date not null,
Gender varchar(10) not null,
Email varchar(100) not null unique,
PhoneNumber varchar(20) not null,
Address varchar(300) not null,
Position varchar(30) not null,
Joiningdate date not null,
Terminationdate date null);

--Creating Payroll table 
create table payroll(
PayrollId int primary key identity(1,1) ,
EmployeeId int,
PayPeriodStartDate date not null,
PayPeriodEndDate date not null,
Basicsalary decimal not null,
OvertimePay decimal not null,
Deductions decimal not null,
NetSalary as (Basicsalary + OvertimePay - Deductions) 
);
alter table payroll 
add constraint FK_Payroll_Employee 
foreign key (EmployeeId) references Employee(EmployeeId)

--Creating Tax table 
create table Tax (
TaxId int primary key identity(1,1),
EmployeeID int,
TaxYear int not null,
TaxableIncome decimal not null,
TaxAmount decimal not null,
constraint FK_Tax_Employee foreign key (EmployeeID) references Employee(EmployeeID)
);

--Creating Financialrecord table 
create table FinancialRecord(
RecordID int primary key identity(1,1),
EmployeeID int,
RecordDate date not null,
Description varchar(300) not null,
Amount decimal not null,
RecordType varchar(50) not null,
constraint FK_FinancialRecord_Employee foreign key (EmployeeID) references Employee(EmployeeID)
);

--Inserting into employee table 
insert into Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, Joiningdate)
values
('Fiza', 'Saleem', '2000-03-15', 'Female', 'fiza.saleem@payxpert.com', '9876543210', '123 Main St, Chennai', 'Software Engineer', '2023-01-10'),
('Swetha', 'Saravanan', '1999-06-20', 'Female', 'swetha.saravanan@payxpert.com', '9876543211', '456 gandhi St, Mumbai', 'HR Manager', '2022-03-05'),
('Vignesh', 'Palanisamy', '2000-11-30', 'Male', 'vignesh.palanisamy@payxpert.com', '9876543212', '789 silk St, Bengaluru', 'Finance Analyst', '2021-07-01');
select * from Employee

--Inserting into payroll table 
insert into Payroll (EmployeeId, PayPeriodStartDate, PayPeriodEndDate, Basicsalary, OvertimePay, Deductions)
values 
(1, '2024-10-01', '2024-10-31', 50000, 5000, 2000),
(2, '2024-10-01', '2024-10-31', 60000, 7000, 3000),
(3, '2024-10-01', '2024-10-31', 55000, 6000, 2500);
select * from payroll

--Inserting into tax table 
insert into Tax(EmployeeID, TaxYear, TaxableIncome, TaxAmount)
values
(1, 2024, 53000, 10000),
(2, 2024, 67000, 12000),
(3, 2024, 58500, 11000);

select * from Tax

--Inserting into FinancialRecord table 
insert into FinancialRecord(EmployeeID, RecordDate, Description, Amount, RecordType)
values
(1, '2024-11-01', 'Medical Reimbursement', 2000, 'Expense'),
(2, '2024-11-05', 'Travel Allowance', 3000, 'Expense'),
(3, '2024-11-08', 'Year-End Bonus', 10000, 'Income');
select * from FinancialRecord
