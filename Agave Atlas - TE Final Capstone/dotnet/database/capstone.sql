USE master
GO

--drop database if it exists
--IF DB_ID('final_capstone') IS NOT NULL
--BEGIN
--	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--	DROP DATABASE final_capstone;
--END

DROP DATABASE IF EXISTS final_capstone;
GO

CREATE DATABASE final_capstone;
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id),
	CONSTRAINT UQ_username UNIQUE (username)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

--populate drink and restaurant data
-- TO POPULATE HARD CODED DATA: 

-- get the file paths to the corresponding files in the database folder and paste them in their correct locations.
-- run the code from here to the end! and that's all folks

USE final_capstone
GO

BEGIN TRANSACTION

DROP TABLE IF EXISTS restaurant_drinks;
DROP TABLE IF EXISTS restaurant_review;
DROP TABLE IF EXISTS drinks;
DROP TABLE IF EXISTS restaurants;
DROP TABLE IF EXISTS reviews;


CREATE TABLE drinks (
drink_id int IDENTITY (1,1) NOT NULL,
drink_name VARCHAR (50) NOT NULL,
description TEXT,
isFrozen TINYINT NOT NULL,
PRIMARY KEY (drink_id)
);

CREATE TABLE restaurants (
restaurant_id int IDENTITY (1,1) NOT NULL,
name VARCHAR (50) NOT NULL,
zip_code VARCHAR(5) NOT NULL,
yelp_id VARCHAR (30) NOT NULL,
phone VARCHAR(15),
display_phone VARCHAR(15),
address VARCHAR(30),
city VARCHAR(30),
state VARCHAR(2),
country VARCHAR(2),
rating numeric(3,2),
review_count int,
is_closed TINYINT,
latitude decimal(9,6),
longitude decimal(9,6),
image_url VARCHAR(75),
yelp_url VARCHAR(225),

PRIMARY KEY (restaurant_id)
);

CREATE TABLE reviews (
review_id int IDENTITY (1,1) NOT NULL,
user_id int NOT NULL,
rating int,
review_text TEXT,
PRIMARY KEY (review_id),
FOREIGN KEY (user_id) REFERENCES users(user_id),
CONSTRAINT Rating_Ck CHECK (rating IN (1,2,3,4,5))
);

CREATE TABLE restaurant_review (
review_id int NOT NULL,
restaurant_id int NOT NULL,
PRIMARY KEY (review_id, restaurant_id),
FOREIGN KEY (review_id) REFERENCES reviews(review_id),
FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id));

CREATE TABLE drink_review (
review_id int NOT NULL,
drink_id int NOT NULL,
PRIMARY KEY (review_id, drink_id),
FOREIGN KEY (review_id) REFERENCES reviews(review_id),
FOREIGN KEY (drink_id) REFERENCES drinks(drink_id));

CREATE TABLE restaurant_drinks (
drink_id INT NOT NULL,
restaurant_id INT NOT NULL,
PRIMARY KEY (drink_id, restaurant_id),
FOREIGN KEY (drink_id) REFERENCES drinks(drink_id),
FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id)
);
COMMIT;
GO

BEGIN TRANSACTION
--Partner Projects\
BULK INSERT dbo.drinks 
FROM "C:\Users\Student\workspace\c-sharp-finalcapstone-team1\dotnet\database\Drinks.csv"
WITH (FORMAT = 'CSV',
FIRSTROW = 2,
FIELDTERMINATOR = ',',
ROWTERMINATOR = ';'
);

BULK INSERT dbo.restaurants
FROM "C:\Users\Student\workspace\c-sharp-finalcapstone-team1\dotnet\database\Restaurants.csv"
WITH (FORMAT = 'CSV',
FIRSTROW = 2,
FIELDTERMINATOR = ',',
ROWTERMINATOR = ';'
);


BULK INSERT dbo.restaurant_drinks
FROM "C:\Users\Student\workspace\c-sharp-finalcapstone-team1\dotnet\database\drink_restaurant.csv"
WITH (FORMAT = 'CSV',
FIRSTROW = 2,
FIELDTERMINATOR = ',',
ROWTERMINATOR = ';'
);

COMMIT;

--SELECT drinks.drink_id, drink_name, description FROM drinks 
--JOIN restaurant_drinks ON drinks.drink_id = restaurant_drinks.drink_id
--JOIN restaurants ON restaurants.restaurant_id = restaurant_drinks.restaurant_id
--WHERE name LIKE 'bar%' ;

USE final_capstone
GO
ALTER TABLE restaurants
ADD image_file_path VARCHAR(300);
USE final_capstone
GO
