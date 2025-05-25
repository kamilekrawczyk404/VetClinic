-- MySQL dump 10.13  Distrib 8.0.36, for macos14 (arm64)
--
-- Host: localhost    Database: vetclinic
-- ------------------------------------------------------
-- Server version	9.2.0

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
-- Table structure for table `Admin`
--

DROP TABLE IF EXISTS `Admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Admin` (
  `id` int DEFAULT NULL,
  `name` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `surname` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  KEY `fk_admin_user` (`id`),
  CONSTRAINT `fk_admin_user` FOREIGN KEY (`id`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Admin`
--

LOCK TABLES `Admin` WRITE;
/*!40000 ALTER TABLE `Admin` DISABLE KEYS */;
/*!40000 ALTER TABLE `Admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Appointment`
--

DROP TABLE IF EXISTS `Appointment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Appointment` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pet_id` int NOT NULL,
  `doctor_id` int NOT NULL,
  `reason_for_visit` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `notes` varchar(255) COLLATE utf8mb3_polish_ci DEFAULT NULL,
  `diagnosis` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `appointment_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_appointment_pet` (`pet_id`),
  KEY `fk_appointment_doctor` (`doctor_id`),
  CONSTRAINT `fk_appointment_doctor` FOREIGN KEY (`doctor_id`) REFERENCES `Doctor` (`id`),
  CONSTRAINT `fk_appointment_pet` FOREIGN KEY (`pet_id`) REFERENCES `Pet` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Appointment`
--

LOCK TABLES `Appointment` WRITE;
/*!40000 ALTER TABLE `Appointment` DISABLE KEYS */;
/*!40000 ALTER TABLE `Appointment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AppointmentServices`
--

DROP TABLE IF EXISTS `AppointmentServices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AppointmentServices` (
  `id` int NOT NULL AUTO_INCREMENT,
  `appointment_id` int DEFAULT NULL,
  `service_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_appointment_service` (`appointment_id`),
  KEY `fk_service_appointment` (`service_id`),
  CONSTRAINT `fk_appointment_service` FOREIGN KEY (`appointment_id`) REFERENCES `Appointment` (`id`),
  CONSTRAINT `fk_service_appointment` FOREIGN KEY (`service_id`) REFERENCES `Service` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppointmentServices`
--

LOCK TABLES `AppointmentServices` WRITE;
/*!40000 ALTER TABLE `AppointmentServices` DISABLE KEYS */;
/*!40000 ALTER TABLE `AppointmentServices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Client`
--

DROP TABLE IF EXISTS `Client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Client` (
  `id` int NOT NULL,
  `name` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `surname` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_client_user` FOREIGN KEY (`id`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Client`
--

LOCK TABLES `Client` WRITE;
/*!40000 ALTER TABLE `Client` DISABLE KEYS */;
/*!40000 ALTER TABLE `Client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Doctor`
--

DROP TABLE IF EXISTS `Doctor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Doctor` (
  `id` int NOT NULL,
  `name` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `surname` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `specialization` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `description` varchar(255) COLLATE utf8mb3_polish_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_doctor_user` FOREIGN KEY (`id`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Doctor`
--

LOCK TABLES `Doctor` WRITE;
/*!40000 ALTER TABLE `Doctor` DISABLE KEYS */;
/*!40000 ALTER TABLE `Doctor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Drug`
--

DROP TABLE IF EXISTS `Drug`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Drug` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `dosage_form` enum('Tablet','Liquid','Injection') COLLATE utf8mb3_polish_ci NOT NULL,
  `strength` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `unit_of_measure` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `description` varchar(255) COLLATE utf8mb3_polish_ci DEFAULT NULL,
  `manufacturer` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `stock_quantity` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Drug`
--

LOCK TABLES `Drug` WRITE;
/*!40000 ALTER TABLE `Drug` DISABLE KEYS */;
/*!40000 ALTER TABLE `Drug` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Opinion`
--

DROP TABLE IF EXISTS `Opinion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Opinion` (
  `id` int NOT NULL AUTO_INCREMENT,
  `client_id` int NOT NULL,
  `doctor_id` int NOT NULL,
  `comment` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `rating` smallint NOT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_opinion_client` (`client_id`),
  KEY `fk_opinion_doctor` (`doctor_id`),
  CONSTRAINT `fk_opinion_client` FOREIGN KEY (`client_id`) REFERENCES `Client` (`id`),
  CONSTRAINT `fk_opinion_doctor` FOREIGN KEY (`doctor_id`) REFERENCES `Doctor` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Opinion`
--

LOCK TABLES `Opinion` WRITE;
/*!40000 ALTER TABLE `Opinion` DISABLE KEYS */;
/*!40000 ALTER TABLE `Opinion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Pet`
--

DROP TABLE IF EXISTS `Pet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Pet` (
  `id` int NOT NULL AUTO_INCREMENT,
  `client_id` int NOT NULL,
  `name` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `species` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `breed` varchar(255) COLLATE utf8mb3_polish_ci DEFAULT NULL,
  `weight` decimal(10,0) NOT NULL,
  `gender` enum('Male','Female','Neutered','Spayed') COLLATE utf8mb3_polish_ci NOT NULL,
  `date_of_birth` date NOT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_pet_client` (`client_id`),
  CONSTRAINT `fk_pet_client` FOREIGN KEY (`client_id`) REFERENCES `Role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Pet`
--

LOCK TABLES `Pet` WRITE;
/*!40000 ALTER TABLE `Pet` DISABLE KEYS */;
/*!40000 ALTER TABLE `Pet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Prescription`
--

DROP TABLE IF EXISTS `Prescription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Prescription` (
  `id` int NOT NULL AUTO_INCREMENT,
  `appointment_id` int NOT NULL,
  `instructions` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `expiry_date` datetime NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_prescription_appointment` (`appointment_id`),
  CONSTRAINT `fk_prescription_appointment` FOREIGN KEY (`appointment_id`) REFERENCES `Appointment` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Prescription`
--

LOCK TABLES `Prescription` WRITE;
/*!40000 ALTER TABLE `Prescription` DISABLE KEYS */;
/*!40000 ALTER TABLE `Prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PrescriptionDrugs`
--

DROP TABLE IF EXISTS `PrescriptionDrugs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PrescriptionDrugs` (
  `id` int NOT NULL AUTO_INCREMENT,
  `prescription_id` int NOT NULL,
  `drug_id` int NOT NULL,
  `quantity` decimal(10,0) NOT NULL,
  `dosage_instructions` varchar(255) COLLATE utf8mb3_polish_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_drug_prescription` (`drug_id`),
  KEY `fk_prescription_dug` (`prescription_id`),
  CONSTRAINT `fk_drug_prescription` FOREIGN KEY (`drug_id`) REFERENCES `Drug` (`id`),
  CONSTRAINT `fk_prescription_dug` FOREIGN KEY (`prescription_id`) REFERENCES `Prescription` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PrescriptionDrugs`
--

LOCK TABLES `PrescriptionDrugs` WRITE;
/*!40000 ALTER TABLE `PrescriptionDrugs` DISABLE KEYS */;
/*!40000 ALTER TABLE `PrescriptionDrugs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Role`
--

DROP TABLE IF EXISTS `Role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Role` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(6) COLLATE utf8mb3_polish_ci NOT NULL DEFAULT 'client',
  `description` text COLLATE utf8mb3_polish_ci,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role`
--

LOCK TABLES `Role` WRITE;
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
/*!40000 ALTER TABLE `Role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Service`
--

DROP TABLE IF EXISTS `Service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Service` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `description` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `price` decimal(10,0) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Service`
--

LOCK TABLES `Service` WRITE;
/*!40000 ALTER TABLE `Service` DISABLE KEYS */;
/*!40000 ALTER TABLE `Service` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User` (
  `id` int NOT NULL AUTO_INCREMENT,
  `role_id` int DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb3_polish_ci NOT NULL,
  `gender` enum('Female','Male') COLLATE utf8mb3_polish_ci NOT NULL,
  `telephone` varchar(20) COLLATE utf8mb3_polish_ci NOT NULL,
  `date_of_birth` date NOT NULL,
  `created_at` datetime NOT NULL DEFAULT (now()),
  `last_login` datetime DEFAULT (now()),
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  KEY `fk_user_role` (`role_id`),
  CONSTRAINT `fk_user_role` FOREIGN KEY (`role_id`) REFERENCES `Role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_polish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-21 22:04:27

-- 1. Insert Roles
INSERT INTO `Role` (`id`, `name`, `description`) VALUES
(1, 'admin', 'System administrator'),
(2, 'doctor', 'Veterinarian'),
(3, 'client', 'Pet owner');

-- 2. Insert Users, Clients, Doctors, Admins
-- (Using specific IDs to control relationships for minimum records)

-- User IDs: 101-105 for Admins, 201-205 for Doctors, 301-310 for Clients

-- Admins (5)
INSERT INTO `User` (`id`, `role_id`, `email`, `password`, `gender`, `telephone`, `date_of_birth`, `created_at`, `last_login`) VALUES
(101, 1, 'admin1@vetclinic.com', 'password', 'Male', '111222333', '1985-01-15', NOW(), NOW()),
(102, 1, 'admin2@vetclinic.com', 'password', 'Female', '111222334', '1990-03-22', NOW(), NOW()),
(103, 1, 'admin3@vetclinic.com', 'password', 'Male', '111222335', '1978-07-01', NOW(), NOW()),
(104, 1, 'admin4@vetclinic.com', 'password', 'Female', '111222336', '1992-11-05', NOW(), NOW()),
(105, 1, 'admin5@vetclinic.com', 'password', 'Male', '1989-09-10', '111222337', NOW(), NOW());

INSERT INTO `Admin` (`id`, `name`, `surname`) VALUES
(101, 'Adam', 'Administrator'),
(102, 'Anna', 'Adminska'),
(103, 'Artur', 'Adminowski'),
(104, 'Alicja', 'Admiczek'),
(105, 'Andrzej', 'Administratywny');

-- Doctors (5 doctors)
INSERT INTO `User` (`id`, `role_id`, `email`, `password`, `gender`, `telephone`, `date_of_birth`, `created_at`, `last_login`) VALUES
(201, 2, 'dr.kowalski@vetclinic.com', 'password', 'Male', '501234567', '1975-04-01', NOW(), NOW()),
(202, 2, 'dr.nowak@vetclinic.com', 'password', 'Female', '502345678', '1980-08-10', NOW(), NOW()),
(203, 2, 'dr.wisniewski@vetclinic.com', 'password', 'Male', '503456789', '1982-11-20', NOW(), NOW()),
(204, 2, 'dr.zielinska@vetclinic.com', 'password', 'Female', '504567890', '1988-02-28', NOW(), NOW()),
(205, 2, 'dr.dabrowski@vetclinic.com', 'password', 'Male', '505678901', '1970-06-15', NOW(), NOW());

INSERT INTO `Doctor` (`id`, `name`, `surname`, `specialization`, `description`) VALUES
(201, 'Jan', 'Kowalski', 'Surgeon', 'Experienced veterinary surgeon'),
(202, 'Anna', 'Nowak', 'Internist', 'Specialist in internal diseases'),
(203, 'Piotr', 'Wisniewski', 'Orthopedist', 'Expert in animal orthopedics'),
(204, 'Maria', 'Zielinska', 'Dermatologist', 'Treatment of skin diseases in animals'),
(205, 'Adam', 'Dabrowski', 'Cardiologist', 'Heart disease specialist');

-- Clients (10 clients)
INSERT INTO `User` (`id`, `role_id`, `email`, `password`, `gender`, `telephone`, `date_of_birth`, `created_at`, `last_login`) VALUES
(301, 3, 'client1@example.com', 'password', 'Female', '601111111', '1995-05-10', NOW(), NOW()),
(302, 3, 'client2@example.com', 'password', 'Male', '602222222', '1980-01-20', NOW(), NOW()),
(303, 3, 'client3@example.com', 'password', 'Female', '603333333', '1970-11-30', NOW(), NOW()),
(304, 3, 'client4@example.com', 'password', 'Male', '604444444', '1988-04-05', NOW(), NOW()),
(305, 3, 'client5@example.com', 'password', 'Female', '605555555', '1992-07-18', NOW(), NOW()),
(306, 3, 'client6@example.com', 'password', 'Male', '606666666', '1975-09-23', NOW(), NOW()),
(307, 3, 'client7@example.com', 'password', 'Female', '607777777', '1983-02-14', NOW(), NOW()),
(308, 3, 'client8@example.com', 'password', 'Male', '608888888', '1998-12-01', NOW(), NOW()),
(309, 3, 'client9@example.com', 'password', 'Female', '609999999', '1965-06-08', NOW(), NOW()),
(310, 3, 'client10@example.com', 'password', 'Male', '610000000', '1990-03-17', NOW(), NOW());

INSERT INTO `Client` (`id`, `name`, `surname`) VALUES
(301, 'Ewa', 'Kowalska'),
(302, 'Marek', 'Dudek'),
(303, 'Zofia', 'Lis'),
(304, 'Krzysztof', 'Wojcik'),
(305, 'Magdalena', 'Kaczmarek'),
(306, 'Tomasz', 'Pawlak'),
(307, 'Alicja', 'Mazurek'),
(308, 'Michal', 'Szymanski'),
(309, 'Barbara', 'Wesolowska'),
(310, 'Robert', 'Krawczyk');


-- 3. Insert Pets
-- Each client will have at least one pet.
-- NOTE: Your DDL for `Pet` has a foreign key `fk_pet_client` referencing `Role` (`id`).
-- This is unusual; typically, it should reference `Client` (`id`).
-- The data below assumes it *should* reference `Client` (`id`).
-- If your `fk_pet_client` truly means `Role.id`, then `client_id` values here must be 1, 2, or 3.
-- For a standard setup, change your DDL to:
-- CONSTRAINT `fk_pet_client` FOREIGN KEY (`client_id`) REFERENCES `Client` (`id`)
INSERT INTO `Pet` (`id`, `client_id`, `name`, `species`, `breed`, `weight`, `gender`, `date_of_birth`, `created_at`) VALUES
(1, 301, 'Buddy', 'Dog', 'Mixed Breed', 15, 'Male', '2020-03-01', NOW()),
(2, 301, 'Whiskers', 'Cat', 'Persian', 4, 'Female', '2021-06-10', NOW()),
(3, 302, 'Max', 'Dog', 'Labrador', 30, 'Male', '2019-01-20', NOW()),
(4, 302, 'Bella', 'Dog', 'Dachshund', 8, 'Neutered', '2022-04-15', NOW()),
(5, 303, 'Mittens', 'Cat', 'Siberian', 5, 'Spayed', '2020-09-01', NOW()),
(6, 304, 'Fluffy', 'Cat', 'Domestic Shorthair', 3, 'Female', '2023-02-28', NOW()),
(7, 305, 'Charlie', 'Dog', 'German Shepherd', 40, 'Male', '2018-07-01', NOW()),
(8, 306, 'Daisy', 'Dog', 'Beagle', 12, 'Neutered', '2021-11-11', NOW()),
(9, 307, 'Smokey', 'Cat', 'Ragdoll', 6, 'Spayed', '2022-01-05', NOW()),
(10, 308, 'Spike', 'Reptile', 'Iguana', 1, 'Male', '2023-08-01', NOW()),
(11, 309, 'Storm', 'Dog', 'Border Collie', 20, 'Male', '2019-10-25', NOW()),
(12, 310, 'Dot', 'Rabbit', 'Mini Lop', 2, 'Female', '2024-03-10', NOW());


-- 4. Insert Services (at least 20)
INSERT INTO `Service` (`id`, `name`, `description`, `price`) VALUES
(1, 'Veterinary Consultation', 'General check-up visit', 80),
(2, 'Rabies Vaccination', 'Mandatory vaccination', 100),
(3, 'Deworming', 'Administration of anti-parasitic treatment', 50),
(4, 'Blood Test', 'Basic blood panel', 120),
(5, 'Urine Analysis', 'Analysis of urine sample', 70),
(6, 'Dental Scaling', 'Dental procedure under anesthesia', 300),
(7, 'Sterilization/Neutering - Cat', 'Surgical procedure for cats', 400),
(8, 'Sterilization/Neutering - Small Dog', 'Surgical procedure for small dogs', 500),
(9, 'Sterilization/Neutering - Large Dog', 'Surgical procedure for large dogs', 800),
(10, 'X-ray - Single Projection', 'Radiographic image', 150),
(11, 'Abdominal Ultrasound', 'Ultrasound examination', 200),
(12, 'Fecal Examination', 'Parasitological diagnosis', 60),
(13, 'Microchipping', 'Microchip implantation', 90),
(14, 'Nail Trimming', 'Basic grooming procedure', 30),
(15, 'Ear Cleaning', 'Ear hygiene', 40),
(16, 'Wound Treatment', 'Dressing, disinfection', 100),
(17, 'Home Visit', 'Veterinarian visit to client''s home', 250),
(18, 'Veterinary Dietetics', 'Consultation and nutritional plan', 180),
(19, 'Rehabilitation', 'Physiotherapy treatments', 200),
(20, 'Therapeutic Bath', 'Bath with dermatological preparations', 120),
(21, 'Ophthalmological Exam', 'Comprehensive eye examination', 170),
(22, 'Cardiological Exam', 'Consultation with a cardiologist', 220),
(23, 'Allergy Tests', 'Food/environmental allergy testing', 350),
(24, 'Euthanasia', 'Painless ending of life for an animal', 250);


-- 5. Insert Drugs
INSERT INTO `Drug` (`id`, `name`, `dosage_form`, `strength`, `unit_of_measure`, `description`, `manufacturer`, `stock_quantity`) VALUES
(1, 'Amoxicillin', 'Tablet', '250mg', 'mg', 'Broad-spectrum antibiotic', 'VetPharma', 500),
(2, 'Meloxicam', 'Liquid', '1.5mg/ml', 'ml', 'Non-steroidal anti-inflammatory drug', 'Animed', 200),
(3, 'Fipronil Spray', 'Liquid', '0.25%', 'ml', 'Flea and tick preparation', 'PetGuard', 150),
(4, 'Insulin Vet', 'Injection', '100 IU/ml', 'ml', 'Insulin for animals', 'DiabetesCare', 50),
(5, 'Prednisolone', 'Tablet', '5mg', 'mg', 'Corticosteroid', 'PharmVet', 300),
(6, 'Metronidazole', 'Tablet', '500mg', 'mg', 'Antibiotic, antiparasitic drug', 'VetMed', 400),
(7, 'Oxycodone', 'Liquid', '5mg/ml', 'ml', 'Strong analgesic', 'PainRelief', 75),
(8, 'Gabapentin', 'Tablet', '100mg', 'mg', 'Anticonvulsant and pain reliever', 'NeuroVet', 250),
(9, 'Thyroxine', 'Tablet', '0.1mg', 'mg', 'Thyroid hormone replacement', 'HormonePet', 180),
(10, 'Enrofloxacin', 'Injection', '50mg/ml', 'ml', 'Antibiotic (fluoroquinolone)', 'BioVet', 100);


-- 6. Insert Appointments and Prescriptions (past 3 weeks from today: 2025-05-24)
-- This means appointments from 2025-05-03 to 2025-05-24

-- Loop through each doctor (201-205) and each pet (1-12) to ensure at least 10 records per doctor/pet
DELIMITER $$
CREATE PROCEDURE GenerateAppointments()
BEGIN
    DECLARE i INT DEFAULT 0;
    DECLARE j INT DEFAULT 0;
    DECLARE appt_date DATETIME;
    DECLARE reason VARCHAR(255);
    DECLARE notes VARCHAR(255);
    DECLARE diagnosis VARCHAR(255);
    DECLARE doctor_id INT;
    DECLARE pet_id INT;
    DECLARE start_date DATETIME;
    SET start_date = STR_TO_DATE('2025-05-03 09:00:00', '%Y-%m-%d %H:%i:%s'); -- Start date for 3 weeks ago

    -- Generate a sufficient number of appointments to meet the "at least 10 per doctor/pet" requirement
    -- We'll aim for about 200 appointments for good distribution.
    SET i = 0;
    WHILE i < 200 DO -- Generate 200 appointments
        SET doctor_id = 201 + (i % 5); -- Cycle through doctors 201-205
        SET pet_id = 1 + (i % 12);     -- Cycle through pets 1-12

        -- Random time within the 3-week window
        SET appt_date = DATE_ADD(start_date, INTERVAL FLOOR(RAND() * 21 * 24 * 4) MINUTE); -- Random time within 21 days
        SET appt_date = DATE_FORMAT(appt_date, '%Y-%m-%d %H:%i:00'); -- Ensure seconds are 00

        SET reason = ELT(1 + FLOOR(RAND() * 5), 'Routine check-up', 'Cough and sniffles', 'Skin problems', 'Limping', 'Low energy');
        SET notes = ELT(1 + FLOOR(RAND() * 5), 'Animal was calm', 'Required calming', 'Cooperative during examination', 'Slightly scared', 'No specific remarks');
        SET diagnosis = ELT(1 + FLOOR(RAND() * 5), 'Upper respiratory tract inflammation', 'Allergic dermatitis', 'Sprain', 'Bacterial infection', 'Ear infection');

        INSERT INTO `Appointment` (`pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
        (pet_id, doctor_id, reason, notes, diagnosis, appt_date, appt_date);

        -- Also generate a prescription for about 80% of appointments
        IF RAND() < 0.8 THEN
            INSERT INTO `Prescription` (`appointment_id`, `instructions`, `expiry_date`, `created_at`) VALUES
            (LAST_INSERT_ID(), CONCAT('Administer ', ELT(1 + FLOOR(RAND() * 3), 'twice daily', 'once daily', 'every other day'), ' for 7 days.'), DATE_ADD(appt_date, INTERVAL 30 DAY), appt_date);
            
            -- Also generate 1-3 drugs for the prescription
            SET j = 0;
            WHILE j < (1 + FLOOR(RAND() * 3)) DO
                INSERT INTO `PrescriptionDrugs` (`prescription_id`, `drug_id`, `quantity`, `dosage_instructions`) VALUES
                (LAST_INSERT_ID(), 1 + FLOOR(RAND() * 10), 1 + FLOOR(RAND() * 10), CONCAT('Use as directed, ', ELT(1 + FLOOR(RAND() * 3), 'after meal', 'before meal', 'regardless of meal')));
                SET j = j + 1;
            END WHILE;
        END IF;

        -- Add 1-2 services for about 90% of appointments
        IF RAND() < 0.9 THEN
            SET j = 0;
            WHILE j < (1 + FLOOR(RAND() * 2)) DO
                -- Link to current or previous appointment for robustness in case of sparse data
                INSERT INTO `AppointmentServices` (`appointment_id`, `service_id`) VALUES
                (LAST_INSERT_ID() - IF(RAND() < 0.8, 0, 1), 1 + FLOOR(RAND() * 24)); 
                SET j = j + 1;
            END WHILE;
        END IF;

        SET i = i + 1;
    END WHILE;
END$$
DELIMITER ;

CALL GenerateAppointments();
DROP PROCEDURE GenerateAppointments;


-- 7. Insert Opinions (each client comments on each doctor within the past 3 weeks)
-- 10 clients * 5 doctors = 50 opinions
DELIMITER $$
CREATE PROCEDURE GenerateOpinions()
BEGIN
    DECLARE client_id_val INT;
    DECLARE doctor_id_val INT;
    DECLARE comment_text VARCHAR(255);
    DECLARE rating_val SMALLINT;
    DECLARE opinion_date DATETIME;
    DECLARE start_date DATETIME;
    
    SET start_date = STR_TO_DATE('2025-05-03 09:00:00', '%Y-%m-%d %H:%i:%s'); -- Start date for 3 weeks ago

    -- Cursor for clients
    DECLARE client_cursor CURSOR FOR SELECT id FROM Client;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET @client_not_found = TRUE;
    
    -- Cursor for doctors
    DECLARE doctor_cursor CURSOR FOR SELECT id FROM Doctor;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET @doctor_not_found = TRUE;

    -- Initialize flags
    SET @client_not_found = FALSE;
    SET @doctor_not_found = FALSE;

    OPEN client_cursor;
    client_loop: LOOP
        FETCH client_cursor INTO client_id_val;
        IF @client_not_found THEN LEAVE client_loop; END IF;

        OPEN doctor_cursor;
        doctor_loop: LOOP
            FETCH doctor_cursor INTO doctor_id_val;
            IF @doctor_not_found THEN LEAVE doctor_loop; END IF;

            SET opinion_date = DATE_ADD(start_date, INTERVAL FLOOR(RAND() * 21 * 24 * 4) MINUTE); -- Random time within 21 days
            SET opinion_date = DATE_FORMAT(opinion_date, '%Y-%m-%d %H:%i:00');

            SET rating_val = 3 + FLOOR(RAND() * 3); -- Rating 3, 4, or 5
            SET comment_text = ELT(1 + FLOOR(RAND() * 5),
                                   'Very professional visit.',
                                   'A truly dedicated veterinarian, highly recommended!',
                                   'Friendly service and effective help.',
                                   'Doctor explained everything very thoroughly.',
                                   'My pet felt comfortable.');

            INSERT INTO `Opinion` (`client_id`, `doctor_id`, `comment`, `rating`, `created_at`) VALUES
            (client_id_val, doctor_id_val, comment_text, rating_val, opinion_date);

        END LOOP doctor_loop;
        CLOSE doctor_cursor;
        SET @doctor_not_found = FALSE; -- Reset flag for the next client's doctor loop
    END LOOP client_loop;
    CLOSE client_cursor;
END$$
DELIMITER ;

CALL GenerateOpinions();
DROP PROCEDURE GenerateOpinions;


-- Enable foreign key checks and commit changes
SET FOREIGN_KEY_CHECKS = 1;
COMMIT;