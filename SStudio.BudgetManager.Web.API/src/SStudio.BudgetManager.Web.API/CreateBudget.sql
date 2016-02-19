CREATE TABLE Categories
(Id serial primary key,
Name varchar(25) NOT NULL,
Description varchar(100) DEFAULT NULL,
LastUpdated timestamp with time zone NOT NULL
);

CREATE TABLE Users
(Id serial primary key,
FirstName varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
Email varchar(255) DEFAULT NULL,
Phone varchar(20) DEFAULT NULL,
CreateDate timestamp with time zone NOT NULL,
LastUpdated timestamp with time zone NOT NULL
);

CREATE TABLE Actions
(
Id serial primary key,
UserId int REFERENCES Users(Id),
CategoryId int REFERENCES Categories(Id),
Summary decimal(30) NOT NULL,
CreateDate timestamp with time zone NOT NULL,
LastUpdated timestamp with time zone NOT NULL
)
