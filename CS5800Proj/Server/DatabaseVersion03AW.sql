﻿-- MySQL dump 10.13  Distrib 8.0.22, for macos10.15 (x86_64)
--
-- Host: localhost    Database: TestDB
-- ------------------------------------------------------
-- Server version	8.0.23


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Requests`
--

DROP TABLE IF EXISTS `Requests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Requests` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `DisasterLocation` varchar(45) DEFAULT NULL,
  `DisasterDate` varchar(45) DEFAULT NULL,
  `Recipient` varchar(45) DEFAULT NULL,
  `RequestType` varchar(45) DEFAULT NULL,
  `RequestQuantity` varchar(45) DEFAULT NULL,
  `Fulfilled` tinyint DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Table structure for table `donationitems`
--

DROP TABLE IF EXISTS `donationitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `donationitems` (
  `donorLocation` tinytext NOT NULL,
  `donationCat` tinytext NOT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `donationAmount` int(10) unsigned zerofill NOT NULL,
  `donationRequest` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `testUser`
--

DROP TABLE IF EXISTS `testUser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `testUser` (
  `id` int NOT NULL AUTO_INCREMENT,
  `userName` char(25) DEFAULT NULL,
  `passWord` varchar(25) DEFAULT NULL,
  `userType` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Table structure for table `testEvents`
--

DROP TABLE IF EXISTS `testEvents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `testEvents` (
   `id` int NOT NULL AUTO_INCREMENT,
   `description` char(100) DEFAULT NULL,
   `time` char(10) DEFAULT NULL,
   `location` char(25) DEFAULT NULL,
   `authorization` bool DEFAULT TRUE,
   `material` int DEFAULT 0,
   `resources` int DEFAULT 0,
   `money` int DEFAULT 0,
   `food` int DEFAULT 0,
   `address` char(25),
   `email` char(25),
   `phone` char(11),
   PRIMARY KEY (`id`)
) ENGINE = InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-03-12 15:55:27
SELECT * FROM testEvents;

SELECT * FROM Requests;

SELECT * FROM donationitems;

SELECT material FROM testEvents WHERE recipient = 'testName1';

INSERT INTO testEvents (recipient, time, location, material) values ('testName1','testTime1','testLocation1',100);
UPDATE testEvents SET material = 600 WHERE recipient = 'Austin';

DELETE FROM testEvents WHERE id = 10
