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
  `created_ad` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
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


--
-- DATA INSERTION STARTS HERE
--

-- Insert data into `Role` table first as it's referenced by `User`
INSERT INTO `Role` (`id`, `name`, `description`) VALUES
(1, 'admin', 'Administrator with full system access.'),
(2, 'client', 'Standard client with access to their own pets and appointments.'),
(3, 'doctor', 'Veterinarian with access to appointments and patient records.');

-- Insert data into `User` table next as it's referenced by `Admin`, `Client`, `Doctor`
INSERT INTO `User` (`id`, `role_id`, `email`, `password`, `gender`, `telephone`, `date_of_birth`, `created_at`, `last_login`) VALUES
(1, 1, 'admin@vetclinic.com', 'adminpass123', 'Male', '111222333', '1985-01-15', '2024-01-01 10:00:00', '2025-05-21 21:55:00'),
(2, 2, 'jan.kowalski@email.com', 'clientpass123', 'Male', '444555666', '1990-03-20', '2024-01-05 11:30:00', '2025-05-21 22:00:00'),
(3, 2, 'anna.nowak@email.com', 'clientpass456', 'Female', '777888999', '1992-07-25', '2024-01-10 14:00:00', '2025-05-21 22:02:00'),
(4, 3, 'dr.smith@vetclinic.com', 'doctorpass123', 'Female', '123123123', '1980-04-10', '2024-02-01 09:00:00', '2025-05-21 22:03:00'),
(5, 3, 'dr.jones@vetclinic.com', 'doctorpass456', 'Male', '321321321', '1975-11-01', '2024-02-05 09:30:00', '2025-05-21 22:04:00');

-- Insert data into `Admin` table
INSERT INTO `Admin` (`id`, `name`, `surname`) VALUES
(1, 'System', 'Admin');

-- Insert data into `Client` table
INSERT INTO `Client` (`id`, `name`, `surname`) VALUES
(2, 'Jan', 'Kowalski'),
(3, 'Anna', 'Nowak');

-- Insert data into `Doctor` table
INSERT INTO `Doctor` (`id`, `name`, `surname`, `specialization`, `description`) VALUES
(4, 'Maria', 'Smith', 'Small Animal Surgery', 'Experienced surgeon with a passion for pets.'),
(5, 'David', 'Jones', 'Internal Medicine', 'Specializes in diagnosing and treating complex diseases.');

-- Insert data into `Pet` table (Note: The FK constraint `fk_pet_client` in your dump references `Role.id`. This is likely incorrect and should reference `Client.id`. I'm inserting with `client_id` values that match `Client.id`.)
INSERT INTO `Pet` (`id`, `client_id`, `name`, `species`, `breed`, `weight`, `gender`, `date_of_birth`, `created_at`) VALUES
(1, 2, 'Burek', 'Dog', 'German Shepherd', 35, 'Male', '2020-05-10', '2024-01-10 15:00:00'),
(2, 2, 'Mruczek', 'Cat', 'Siamese', 5, 'Neutered', '2021-02-15', '2024-01-10 15:05:00'),
(3, 3, 'Azor', 'Dog', 'Golden Retriever', 28, 'Spayed', '2019-08-01', '2024-01-12 09:00:00');

-- Insert data into `Service` table
INSERT INTO `Service` (`id`, `name`, `description`, `price`) VALUES
(1, 'Routine Check-up', 'Annual health examination and vaccinations.', 100),
(2, 'Dental Cleaning', 'Professional dental scaling and polishing.', 250),
(3, 'Emergency Consultation', 'Urgent medical assessment for critical conditions.', 150),
(4, 'Surgery - Spay/Neuter', 'Surgical procedure for sterilization.', 400);

-- Insert data into `Appointment` table
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(1, 1, 4, 'Annual check-up and vaccinations', 'Dog is active and healthy.', 'Healthy, up-to-date on vaccines', '2025-05-10 10:00:00', '2025-05-22 10:00:00'),
(2, 2, 5, 'Lethargy and loss of appetite', 'Cat has not eaten for 2 days. Vomiting once.', 'Possible gastrointestinal issue', '2025-05-15 14:30:00', '2025-05-23 14:00:00'),
(3, 3, 4, 'Limpping on hind leg', 'Golden Retriever felt pain after playing.', 'Suspected sprain, needs X-ray', '2025-05-20 09:00:00', '2025-05-24 11:00:00');

-- Insert data into `AppointmentServices` table
INSERT INTO `AppointmentServices` (`id`, `appointment_id`, `service_id`) VALUES
(1, 1, 1),
(2, 2, 3),
(3, 3, 1);

-- Insert data into `Drug` table
INSERT INTO `Drug` (`id`, `name`, `dosage_form`, `strength`, `unit_of_measure`, `description`, `manufacturer`, `stock_quantity`) VALUES
(1, 'Amoxicillin', 'Tablet', '250mg', 'mg', 'Broad-spectrum antibiotic', 'PharmaCo', 500),
(2, 'Meloxicam', 'Liquid', '1.5mg/ml', 'ml', 'NSAID for pain and inflammation', 'VetMed Labs', 200),
(3, 'Flea & Tick Prevention', 'Tablet', '1 tablet', 'tablet', 'Monthly preventative treatment', 'PetHealth Inc.', 100);

-- Insert data into `Prescription` table
INSERT INTO `Prescription` (`id`, `appointment_id`, `instructions`, `expiry_date`, `created_ad`) VALUES
(1, 2, 'Administer with food twice daily for 7 days.', '2025-06-10 00:00:00', '2025-05-23 15:00:00'),
(2, 3, 'Give orally once daily for 5 days. Re-evaluate after X-ray results.', '2025-06-15 00:00:00', '2025-05-24 12:00:00');

-- Insert data into `PrescriptionDrugs` table
INSERT INTO `PrescriptionDrugs` (`id`, `prescription_id`, `drug_id`, `quantity`, `dosage_instructions`) VALUES
(1, 1, 1, 14, 'One 250mg tablet, twice daily with food.'),
(2, 2, 2, 50, '2 ml orally once daily for 5 days.');

-- Insert data into `Opinion` table
INSERT INTO `Opinion` (`id`, `client_id`, `doctor_id`, `comment`, `rating`, `created_at`) VALUES
(1, 2, 4, 'Dr. Smith was excellent, very thorough with Burek.', 5, '2025-05-23 11:00:00'),
(2, 3, 4, 'Azor received great care. Very professional and friendly.', 4, '2025-05-25 10:00:00'),
(3, 2, 5, 'Dr. Jones was helpful but the cat is still recovering.', 3, '2025-05-24 16:00:00');
