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
  CONSTRAINT `fk_pet_client` FOREIGN KEY (`client_id`) REFERENCES `Client` (`id`) -- Corrected FK constraint to reference `Client.id`
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
(5, 3, 'dr.jones@vetclinic.com', 'doctorpass456', 'Male', '321321321', '1975-11-01', '2024-02-05 09:30:00', '2025-05-21 22:04:00'),
(6, 2, 'piotr.zielinski@email.com', 'clientpass789', 'Male', '987654321', '1988-11-05', '2024-03-01 10:00:00', '2025-05-21 22:05:00'),
(7, 2, 'agata.wójcik@email.com', 'clientpassabc', 'Female', '654987321', '1995-09-12', '2024-03-15 11:00:00', '2025-05-21 22:06:00'),
(8, 3, 'dr.brown@vetclinic.com', 'doctorpass789', 'Female', '456789012', '1978-06-20', '2024-04-01 08:30:00', '2025-05-21 22:07:00');


-- Insert data into `Admin` table
INSERT INTO `Admin` (`id`, `name`, `surname`) VALUES
(1, 'System', 'Admin');

-- Insert data into `Client` table
INSERT INTO `Client` (`id`, `name`, `surname`) VALUES
(2, 'Jan', 'Kowalski'),
(3, 'Anna', 'Nowak'),
(6, 'Piotr', 'Zielinski'),
(7, 'Agata', 'Wójcik');

-- Insert data into `Doctor` table
INSERT INTO `Doctor` (`id`, `name`, `surname`, `specialization`, `description`) VALUES
(4, 'Maria', 'Smith', 'Small Animal Surgery', 'Experienced surgeon with a passion for pets.'),
(5, 'David', 'Jones', 'Internal Medicine', 'Specializes in diagnosing and treating complex diseases.'),
(8, 'Olivia', 'Brown', 'Dermatology', 'Expert in skin conditions and allergies.');

-- Insert data into `Pet` table (Note: The FK constraint `fk_pet_client` in your dump references `Role.id`. This is likely incorrect and should reference `Client.id`. I'm inserting with `client_id` values that match `Client.id`.)
INSERT INTO `Pet` (`id`, `client_id`, `name`, `species`, `breed`, `weight`, `gender`, `date_of_birth`, `created_at`) VALUES
(1, 2, 'Burek', 'Dog', 'German Shepherd', 35, 'Male', '2020-05-10', '2024-01-10 15:00:00'),
(2, 2, 'Mruczek', 'Cat', 'Siamese', 5, 'Neutered', '2021-02-15', '2024-01-10 15:05:00'),
(3, 3, 'Azor', 'Dog', 'Golden Retriever', 28, 'Spayed', '2019-08-01', '2024-01-12 09:00:00'),
(4, 6, 'Kropka', 'Cat', 'Domestic Shorthair', 4, 'Female', '2022-03-20', '2024-03-05 16:00:00'),
(5, 7, 'Max', 'Dog', 'Labrador', 30, 'Male', '2018-06-01', '2024-03-18 10:30:00'),
(6, 6, 'Rysiek', 'Rabbit', 'Dutch', 2, 'Male', '2023-01-01', '2024-04-01 11:00:00');

-- Insert data into `Service` table
INSERT INTO `Service` (`id`, `name`, `description`, `price`) VALUES
(1, 'Routine Check-up', 'Annual health examination and vaccinations.', 100),
(2, 'Dental Cleaning', 'Professional dental scaling and polishing.', 250),
(3, 'Emergency Consultation', 'Urgent medical assessment for critical conditions.', 150),
(4, 'Surgery - Spay/Neuter', 'Surgical procedure for sterilization.', 400),
(5, 'Vaccination', 'Administration of necessary vaccines.', 75),
(6, 'X-ray', 'Diagnostic imaging using X-rays.', 120),
(7, 'Blood Test', 'Comprehensive blood work for diagnostic purposes.', 90),
(8, 'Allergy Testing', 'Tests to identify specific allergens in pets.', 180);

-- Insert data into `Appointment` table (Starting IDs from 1 to avoid conflicts with previous data, adjusted based on the new data being generated)
-- Assuming existing appointments had IDs 1, 2, 3. New appointments will start from 4.
LOCK TABLES `Appointment` WRITE;
/*!40000 ALTER TABLE `Appointment` DISABLE KEYS */;
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(1, 1, 4, 'Annual check-up and vaccinations', 'Dog is active and healthy.', 'Healthy, up-to-date on vaccines', '2025-05-10 10:00:00', '2025-05-22 10:00:00'),
(2, 2, 5, 'Lethargy and loss of appetite', 'Cat has not eaten for 2 days. Vomiting once.', 'Possible gastrointestinal issue', '2025-05-15 14:30:00', '2025-05-23 14:00:00'),
(3, 3, 4, 'Limpping on hind leg', 'Golden Retriever felt pain after playing.', 'Suspected sprain, needs X-ray', '2025-05-20 09:00:00', '2025-05-24 11:00:00');

-- Generate appointments from May 1st, 2025, ensuring each doctor has 20+ per week
-- Doctor 4 (Maria Smith)
SET @current_appointment_id = 4;
SET @current_prescription_id = 3;
SET @current_prescription_drug_id = 3;
SET @current_appointment_service_id = 4;
SET @current_opinion_id = 4;

-- Week 1 (May 1st - May 7th)
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
-- Doctor 4 - Week 1
(4, 1, 4, 'Vaccination booster', 'Routine shot, pet active.', 'Vaccinated', '2025-04-28 09:00:00', '2025-05-01 09:00:00'),
(5, 2, 4, 'Flea treatment', 'Heavy flea infestation.', 'Fleas treated', '2025-04-29 10:00:00', '2025-05-01 10:00:00'),
(6, 3, 4, 'Ear infection check', 'Itching ears.', 'Otitis externa', '2025-04-30 11:00:00', '2025-05-01 11:00:00'),
(7, 4, 4, 'Routine check-up', 'Cat seems healthy.', 'Healthy', '2025-04-29 13:00:00', '2025-05-02 09:30:00'),
(8, 5, 4, 'Nail trim', 'Nails overgrown.', 'Nails trimmed', '2025-04-30 14:00:00', '2025-05-02 10:30:00'),
(9, 6, 4, 'Dental check', 'Bad breath.', 'Gingivitis, needs cleaning', '2025-05-01 09:00:00', '2025-05-02 11:30:00'),
(10, 1, 4, 'Follow-up ear infection', 'Improvement noted.', 'Healing well', '2025-05-01 15:00:00', '2025-05-03 09:00:00'),
(11, 2, 4, 'Weight management consultation', 'Owner concerned about weight.', 'Diet plan initiated', '2025-05-02 10:00:00', '2025-05-03 10:00:00'),
(12, 3, 4, 'Annual vaccinations', 'Routine.', 'Vaccinated', '2025-05-02 11:00:00', '2025-05-03 11:00:00'),
(13, 4, 4, 'Lethargy investigation', 'Cat sleeping more than usual.', 'Mild viral infection', '2025-05-03 09:00:00', '2025-05-05 09:00:00'),
(14, 5, 4, 'Deworming', 'Routine deworming.', 'Dewormed', '2025-05-03 10:00:00', '2025-05-05 10:00:00'),
(15, 6, 4, 'Diarrhea', 'Rabbit has soft stools.', 'Dietary indiscretion', '2025-05-04 11:00:00', '2025-05-05 11:00:00'),
(16, 1, 4, 'Skin rash', 'Redness on abdomen.', 'Allergic reaction', '2025-05-04 14:00:00', '2025-05-06 09:00:00'),
(17, 2, 4, 'Eye discharge', 'Sticky eyes.', 'Conjunctivitis', '2025-05-05 10:00:00', '2025-05-06 10:00:00'),
(18, 3, 4, 'Check on limping (X-ray follow-up)', 'Owner reports improvement.', 'Recovering well', '2025-05-05 11:00:00', '2025-05-06 11:00:00'),
(19, 4, 4, 'Appetite loss', 'Not eating well for 2 days.', 'Stress-related', '2025-05-06 09:00:00', '2025-05-07 09:00:00'),
(20, 5, 4, 'Regular check-up', 'Annual visit for Labrador.', 'Healthy', '2025-05-06 10:00:00', '2025-05-07 10:00:00'),
(21, 6, 4, 'Routine check-up', 'Rabbit health check.', 'Healthy', '2025-05-06 14:00:00', '2025-05-07 14:00:00'),
(22, 1, 4, 'Allergy medication refill', 'Ongoing medication.', 'Stable', '2025-05-07 09:00:00', '2025-05-07 15:00:00'),
(23, 2, 4, 'Recheck eye infection', 'Eyes clearing up.', 'Almost fully recovered', '2025-05-07 10:00:00', '2025-05-07 16:00:00');

-- Doctor 5 (David Jones) - Week 1
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(24, 1, 5, 'Digestive upset', 'Vomiting and diarrhea.', 'Gastroenteritis', '2025-04-28 10:00:00', '2025-05-01 09:30:00'),
(25, 2, 5, 'Urinary issues', 'Frequent urination, straining.', 'UTI suspected', '2025-04-29 11:00:00', '2025-05-01 10:30:00'),
(26, 3, 5, 'Chronic cough', 'Coughing for a few weeks.', 'Possible kennel cough', '2025-04-30 12:00:00', '2025-05-01 11:30:00'),
(27, 4, 5, 'Sudden lameness', 'Limping on front paw.', 'Minor sprain', '2025-04-29 14:00:00', '2025-05-02 10:00:00'),
(28, 5, 5, 'Skin irritation', 'Red patches on skin.', 'Allergic dermatitis', '2025-04-30 15:00:00', '2025-05-02 11:00:00'),
(29, 6, 5, 'Loss of balance', 'Stumbling and disoriented.', 'Inner ear infection', '2025-05-01 10:00:00', '2025-05-02 12:00:00'),
(30, 1, 5, 'Recheck gastroenteritis', 'Symptoms subsided.', 'Fully recovered', '2025-05-01 16:00:00', '2025-05-03 09:30:00'),
(31, 2, 5, 'UTI follow-up', 'Urine sample taken.', 'Confirmed UTI, starting antibiotics', '2025-05-02 12:00:00', '2025-05-03 10:30:00'),
(32, 3, 5, 'Kennel cough follow-up', 'Cough less severe.', 'Improving', '2025-05-02 13:00:00', '2025-05-03 11:30:00'),
(33, 4, 5, 'Annual check-up', 'Routine visit for cat.', 'Healthy', '2025-05-03 10:00:00', '2025-05-05 09:30:00'),
(34, 5, 5, 'Behavioral consultation', 'Excessive barking.', 'Anxiety related', '2025-05-03 11:00:00', '2025-05-05 10:30:00'),
(35, 6, 5, 'Eye injury', 'Scratch on eye.', 'Corneal abrasion', '2025-05-04 12:00:00', '2025-05-05 11:30:00'),
(36, 1, 5, 'Routine vaccination', 'Booster shot.', 'Vaccinated', '2025-05-04 15:00:00', '2025-05-06 09:30:00'),
(37, 2, 5, 'Dental check-up', 'Examining teeth.', 'Needs dental cleaning', '2025-05-05 11:00:00', '2025-05-06 10:30:00'),
(38, 3, 5, 'Grooming advise', 'Owner asking for advice on fur care.', 'Advised regular brushing', '2025-05-05 12:00:00', '2025-05-06 11:30:00'),
(39, 4, 5, 'Fever', 'Lethargic with high temperature.', 'Viral infection', '2025-05-06 10:00:00', '2025-05-07 09:30:00'),
(40, 5, 5, 'Arthritis management', 'Senior dog with joint pain.', 'Prescribed joint supplements', '2025-05-06 11:00:00', '2025-05-07 10:30:00'),
(41, 6, 5, 'Follow-up eye injury', 'Eye looks better.', 'Healing well', '2025-05-06 15:00:00', '2025-05-07 14:30:00'),
(42, 1, 5, 'Annual check-up', 'Routine check.', 'Healthy', '2025-05-07 10:00:00', '2025-05-07 15:30:00'),
(43, 2, 5, 'Weight loss program', 'Reviewing diet for overweight cat.', 'Progressing well', '2025-05-07 11:00:00', '2025-05-07 16:30:00');

-- Doctor 8 (Olivia Brown) - Week 1
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(44, 1, 8, 'Allergy flare-up', 'Severe itching and redness.', 'Atopic dermatitis', '2025-04-28 11:00:00', '2025-05-01 10:00:00'),
(45, 2, 8, 'Hot spots', 'Licking and chewing on flank.', 'Bacterial dermatitis', '2025-04-29 12:00:00', '2025-05-01 11:00:00'),
(46, 3, 8, 'Skin growth', 'New lump on side.', 'Benign lipoma', '2025-04-30 13:00:00', '2025-05-01 12:00:00'),
(47, 4, 8, 'Excessive shedding', 'Owner concerned about hair loss.', 'Seasonal shedding', '2025-04-29 15:00:00', '2025-05-02 10:30:00'),
(48, 5, 8, 'Pustules on skin', 'Small bumps with pus.', 'Pyoderma', '2025-04-30 16:00:00', '2025-05-02 11:30:00'),
(49, 6, 8, 'Mite infestation', 'Itching and hair loss around ears.', 'Ear mites', '2025-05-01 11:00:00', '2025-05-02 12:30:00'),
(50, 1, 8, 'Atopic dermatitis follow-up', 'Skin calming down.', 'Improvement, continue medication', '2025-05-01 17:00:00', '2025-05-03 10:00:00'),
(51, 2, 8, 'Hot spots recheck', 'Lesions healing.', 'Healing well', '2025-05-02 13:00:00', '2025-05-03 11:00:00'),
(52, 3, 8, 'Annual skin check', 'Routine examination.', 'No new concerns', '2025-05-02 14:00:00', '2025-05-03 12:00:00'),
(53, 4, 8, 'Allergy testing', 'Preparing for full allergy panel.', 'Scheduled for test', '2025-05-03 11:00:00', '2025-05-05 10:00:00'),
(54, 5, 8, 'Skin infection follow-up', 'Skin less inflamed.', 'Progressing positively', '2025-05-03 12:00:00', '2025-05-05 11:00:00'),
(55, 6, 8, 'Hair loss patches', 'Circular hair loss on back.', 'Fungal infection suspected', '2025-05-04 13:00:00', '2025-05-05 12:00:00'),
(56, 1, 8, 'Food allergy consultation', 'Discussing dietary changes.', 'Recommended hypoallergenic diet', '2025-05-04 16:00:00', '2025-05-06 10:00:00'),
(57, 2, 8, 'Chronic itching', 'Ongoing scratching despite previous treatment.', 'Further diagnostics needed', '2025-05-05 12:00:00', '2025-05-06 11:00:00'),
(58, 3, 8, 'Dull coat', 'Lack of shine in fur.', 'Nutritional deficiency', '2025-05-05 13:00:00', '2025-05-06 12:00:00'),
(59, 4, 8, 'Itchy paws', 'Licking paws excessively.', 'Environmental allergies', '2025-05-06 11:00:00', '2025-05-07 10:00:00'),
(60, 5, 8, 'Skin biopsy results', 'Discussing findings.', 'Negative for malignancy', '2025-05-06 12:00:00', '2025-05-07 11:00:00'),
(61, 6, 8, 'Fungal infection recheck', 'Started medication.', 'Beginning to clear', '2025-05-06 16:00:00', '2025-05-07 15:00:00'),
(62, 1, 8, 'Eczema management', 'Long-term care plan.', 'Ongoing management', '2025-05-07 11:00:00', '2025-05-07 16:00:00'),
(63, 2, 8, 'Follow-up chronic itching', 'Trying new medication.', 'Monitoring response', '2025-05-07 12:00:00', '2025-05-07 17:00:00');

-- Week 2 (May 8th - May 14th)
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
-- Doctor 4 - Week 2
(64, 3, 4, 'Dental Cleaning Prep', 'Pre-anesthetic exam.', 'Ready for dental procedure', '2025-05-07 10:00:00', '2025-05-08 09:00:00'),
(65, 4, 4, 'Post-op check', 'Following minor surgery.', 'Healing well', '2025-05-08 11:00:00', '2025-05-08 10:00:00'),
(66, 5, 4, 'Annual vaccinations', 'Routine.', 'Vaccinated', '2025-05-08 12:00:00', '2025-05-08 11:00:00'),
(67, 6, 4, 'Lump removal consult', 'Discussing surgical options.', 'Scheduled for surgery', '2025-05-08 13:00:00', '2025-05-09 09:30:00'),
(68, 1, 4, 'Coughing', 'Persistent dry cough.', 'Tracheitis', '2025-05-09 09:00:00', '2025-05-09 10:30:00'),
(69, 2, 4, 'Wellness exam', 'Healthy pet check-up.', 'Healthy', '2025-05-09 10:00:00', '2025-05-09 11:30:00'),
(70, 3, 4, 'Dental Cleaning', 'Performing procedure.', 'Successful cleaning', '2025-05-09 11:00:00', '2025-05-10 09:00:00'),
(71, 4, 4, 'Ear cleaning', 'Routine ear maintenance.', 'Ears clean', '2025-05-10 09:00:00', '2025-05-10 10:00:00'),
(72, 5, 4, 'Puppy check-up', 'First vet visit for new puppy.', 'Healthy puppy', '2025-05-10 10:00:00', '2025-05-10 11:00:00'),
(73, 6, 4, 'Follow-up diarrhea', 'Stools firming up.', 'Recovering well', '2025-05-10 11:00:00', '2025-05-12 09:00:00'),
(74, 1, 4, 'Gastrointestinal upset', 'Vomiting, no diarrhea.', 'Dietary indiscretion', '2025-05-11 09:00:00', '2025-05-12 10:00:00'),
(75, 2, 4, 'Skin lump check', 'New lump on leg.', 'Benign', '2025-05-11 10:00:00', '2025-05-12 11:00:00'),
(76, 3, 4, 'Post-dental check', 'Checking recovery.', 'Recovering well', '2025-05-11 11:00:00', '2025-05-13 09:00:00'),
(77, 4, 4, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-05-12 09:00:00', '2025-05-13 10:00:00'),
(78, 5, 4, 'Limpping - acute', 'Suddenly limping on front leg.', 'Sprain', '2025-05-12 10:00:00', '2025-05-13 11:00:00'),
(79, 6, 4, 'Respiratory symptoms', 'Sneezing and nasal discharge.', 'Upper respiratory infection', '2025-05-12 11:00:00', '2025-05-13 12:00:00'),
(80, 1, 4, 'Follow-up cough', 'Cough improving.', 'Almost resolved', '2025-05-13 09:00:00', '2025-05-14 09:00:00'),
(81, 2, 4, 'Microchipping', 'Routine microchip implantation.', 'Microchipped', '2025-05-13 10:00:00', '2025-05-14 10:00:00'),
(82, 3, 4, 'New pet check-up', 'First visit for adopted dog.', 'Healthy', '2025-05-13 11:00:00', '2025-05-14 11:00:00'),
(83, 4, 4, 'Recheck lameness', 'Cat still slightly limping.', 'Needs further rest', '2025-05-13 12:00:00', '2025-05-14 12:00:00'),
(84, 5, 4, 'Senior check-up', 'Comprehensive exam for older dog.', 'Normal for age', '2025-05-14 09:00:00', '2025-05-14 13:00:00'),
(85, 6, 4, 'Weight check', 'Monitoring rabbit weight.', 'Stable', '2025-05-14 10:00:00', '2025-05-14 14:00:00');

-- Doctor 5 (David Jones) - Week 2
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(86, 1, 5, 'Blood test results', 'Discussing previous test results.', 'Normal results', '2025-05-07 11:00:00', '2025-05-08 09:30:00'),
(87, 2, 5, 'UTI recheck', 'Checking on progress of treatment.', 'Improving', '2025-05-08 12:00:00', '2025-05-08 10:30:00'),
(88, 3, 5, 'Lethargy in older dog', 'Owner concerned about energy levels.', 'Age-related slowing', '2025-05-08 13:00:00', '2025-05-08 11:30:00'),
(89, 4, 5, 'Chronic vomiting', 'Intermittent vomiting episodes.', 'Dietary sensitivity', '2025-05-08 14:00:00', '2025-05-09 10:00:00'),
(90, 5, 5, 'Respiratory distress', 'Trouble breathing, wheezing.', 'Asthma suspected', '2025-05-09 10:00:00', '2025-05-09 11:00:00'),
(91, 6, 5, 'Loss of appetite - rabbit', 'Not eating for 24h.', 'Gastrointestinal stasis', '2025-05-09 11:00:00', '2025-05-09 12:00:00'),
(92, 1, 5, 'Wellness check', 'Routine.', 'Healthy', '2025-05-09 12:00:00', '2025-05-10 09:30:00'),
(93, 2, 5, 'Dental scaling consult', 'Discussing dental procedure.', 'Scheduled for cleaning', '2025-05-10 09:00:00', '2025-05-10 10:30:00'),
(94, 3, 5, 'Kidney disease management', 'Monitoring kidney function.', 'Stable', '2025-05-10 10:00:00', '2025-05-10 11:30:00'),
(95, 4, 5, 'Recheck chronic vomiting', 'Symptoms reduced.', 'Improving', '2025-05-10 11:00:00', '2025-05-12 09:30:00'),
(96, 5, 5, 'Asthma follow-up', 'Breathing easier.', 'Medication effective', '2025-05-11 10:00:00', '2025-05-12 10:30:00'),
(97, 6, 5, 'GI stasis follow-up', 'Rabbit eating small amounts.', 'Slowly recovering', '2025-05-11 11:00:00', '2025-05-12 11:30:00'),
(98, 1, 5, 'Urinary incontinence', 'Leaking urine while sleeping.', 'Weak bladder', '2025-05-11 12:00:00', '2025-05-13 09:30:00'),
(99, 2, 5, 'Eye infection', 'Redness and swelling.', 'Bacterial infection', '2025-05-12 10:00:00', '2025-05-13 10:30:00'),
(100, 3, 5, 'Heart murmur recheck', 'Listening to heart.', 'Stable murmur', '2025-05-12 11:00:00', '2025-05-13 11:30:00'),
(101, 4, 5, 'Annual check-up', 'Routine check-up.', 'Healthy', '2025-05-12 12:00:00', '2025-05-13 12:30:00'),
(102, 5, 5, 'Allergy medication review', 'Discussing current medication.', 'Continue current dosage', '2025-05-13 09:00:00', '2025-05-14 09:30:00'),
(103, 6, 5, 'Dental exam', 'Routine dental check for rabbit.', 'Healthy teeth', '2025-05-13 10:00:00', '2025-05-14 10:30:00'),
(104, 1, 5, 'Pain management consult', 'Discussing options for chronic pain.', 'Started new pain medication', '2025-05-13 11:00:00', '2025-05-14 11:30:00'),
(105, 2, 5, 'Recheck eye infection', 'Eye looks clear.', 'Cleared', '2025-05-13 12:00:00', '2025-05-14 12:30:00'),
(106, 3, 5, 'Dietary advice', 'Owner wants to improve dog diet.', 'Recommended high-quality food', '2025-05-14 09:00:00', '2025-05-14 13:30:00'),
(107, 4, 5, 'Behavioral issue', 'Aggression towards other cats.', 'Referred to behaviorist', '2025-05-14 10:00:00', '2025-05-14 14:30:00');

-- Doctor 8 (Olivia Brown) - Week 2
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(108, 3, 8, 'Allergy retest', 'Checking progress of desensitization.', 'Positive response', '2025-05-07 12:00:00', '2025-05-08 10:00:00'),
(109, 4, 8, 'Dermatitis follow-up', 'Skin less inflamed.', 'Improving, continue treatment', '2025-05-08 13:00:00', '2025-05-08 11:00:00'),
(110, 5, 8, 'Chronic itching', 'Ongoing problem.', 'Needs further investigation', '2025-05-08 14:00:00', '2025-05-08 12:00:00'),
(111, 6, 8, 'Skin infection', 'Redness and scabs.', 'Bacterial skin infection', '2025-05-08 15:00:00', '2025-05-09 10:30:00'),
(112, 1, 8, 'Hair loss patches', 'Circular patches, suspect ringworm.', 'Fungal infection confirmed', '2025-05-09 11:00:00', '2025-05-09 11:30:00'),
(113, 2, 8, 'Excessive grooming', 'Licking belly until bald.', 'Behavioral, stress-related', '2025-05-09 12:00:00', '2025-05-09 12:30:00'),
(114, 3, 8, 'Rash on belly', 'Red bumps.', 'Contact dermatitis', '2025-05-09 13:00:00', '2025-05-10 10:00:00'),
(115, 4, 8, 'Skin scraping', 'Diagnostic test for mites.', 'Negative for mites', '2025-05-10 09:00:00', '2025-05-10 11:00:00'),
(116, 5, 8, 'Food allergy test', 'Starting elimination diet.', 'Instruction for owner', '2025-05-10 10:00:00', '2025-05-10 12:00:00'),
(117, 6, 8, 'Dandruff and dry skin', 'Flaky coat.', 'Needs moisturizing shampoo', '2025-05-10 11:00:00', '2025-05-12 10:00:00'),
(118, 1, 8, 'Ringworm recheck', 'Lesions improving.', 'Healing well', '2025-05-11 10:00:00', '2025-05-12 11:00:00'),
(119, 2, 8, 'Behavioral grooming follow-up', 'Less licking observed.', 'Progressing', '2025-05-11 11:00:00', '2025-05-12 12:00:00'),
(120, 3, 8, 'Recheck contact dermatitis', 'Rash clearing.', 'Almost clear', '2025-05-11 12:00:00', '2025-05-13 10:00:00'),
(121, 4, 8, 'Allergy medication adjustment', 'Increasing dosage.', 'Monitoring response', '2025-05-12 10:00:00', '2025-05-13 11:00:00'),
(122, 5, 8, 'Chronic ear infections', 'Recurring issue.', 'Referred to specialist', '2025-05-12 11:00:00', '2025-05-13 12:00:00'),
(123, 6, 8, 'Foot pad irritation', 'Redness and swelling on paws.', 'Environmental irritant', '2025-05-12 12:00:00', '2025-05-13 13:00:00'),
(124, 1, 8, 'Annual skin check', 'Routine.', 'Healthy skin', '2025-05-13 10:00:00', '2025-05-14 10:00:00'),
(125, 2, 8, 'New skin tags', 'Several small growths.', 'Benign', '2025-05-13 11:00:00', '2025-05-14 11:00:00'),
(126, 3, 8, 'Folliculitis', 'Bumps and hair loss.', 'Bacterial infection', '2025-05-13 12:00:00', '2025-05-14 12:00:00'),
(127, 4, 8, 'Suspected flea allergy', 'Itching around tail base.', 'Confirmed flea allergy', '2025-05-13 13:00:00', '2025-05-14 13:00:00'),
(128, 5, 8, 'General skin health advice', 'Owner seeking preventative tips.', 'Advised on diet and grooming', '2025-05-14 09:00:00', '2025-05-14 14:00:00'),
(129, 6, 8, 'Ulcerated skin lesion', 'Sore on leg.', 'Needs topical treatment', '2025-05-14 10:00:00', '2025-05-14 15:00:00');


-- Week 3 (May 15th - May 21st)
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
-- Doctor 4 - Week 3
(130, 1, 4, 'Dental cleaning consult', 'Discussing dental procedure.', 'Scheduled for cleaning', '2025-05-14 10:00:00', '2025-05-15 09:00:00'),
(131, 2, 4, 'Weight check', 'Monitoring progress on diet.', 'Maintaining weight', '2025-05-14 11:00:00', '2025-05-15 10:00:00'),
(132, 3, 4, 'Ear cleaning', 'Routine ear maintenance.', 'Ears clean', '2025-05-14 12:00:00', '2025-05-15 11:00:00'),
(133, 4, 4, 'New pet check-up', 'First visit for adopted cat.', 'Healthy', '2025-05-15 09:00:00', '2025-05-15 12:00:00'),
(134, 5, 4, 'Post-op check', 'Following minor surgery.', 'Healing well', '2025-05-15 10:00:00', '2025-05-16 09:30:00'),
(135, 6, 4, 'Vaccination', 'Routine.', 'Vaccinated', '2025-05-15 11:00:00', '2025-05-16 10:30:00'),
(136, 1, 4, 'Follow-up GI upset', 'Symptoms resolved.', 'Fully recovered', '2025-05-15 12:00:00', '2025-05-16 11:30:00'),
(137, 2, 4, 'Annual check-up', 'Routine.', 'Healthy', '2025-05-16 09:00:00', '2025-05-17 09:00:00'),
(138, 3, 4, 'Lump check', 'New small lump.', 'Benign', '2025-05-16 10:00:00', '2025-05-17 10:00:00'),
(139, 4, 4, 'Behavioral consultation', 'Excessive meowing.', 'Separation anxiety', '2025-05-16 11:00:00', '2025-05-17 11:00:00'),
(140, 5, 4, 'Recheck limping', 'Dog still slightly limping.', 'Needs more rest', '2025-05-16 12:00:00', '2025-05-19 09:00:00'),
(141, 6, 4, 'Snuffles symptoms', 'Sneezing and runny nose.', 'Bacterial infection', '2025-05-17 09:00:00', '2025-05-19 10:00:00'),
(142, 1, 4, 'Allergy medication refill', 'Ongoing medication.', 'Stable', '2025-05-17 10:00:00', '2025-05-19 11:00:00'),
(143, 2, 4, 'Routine dental check', 'Checking teeth.', 'Healthy teeth', '2025-05-17 11:00:00', '2025-05-20 09:00:00'),
(144, 3, 4, 'Ear infection', 'Itching and discharge.', 'Otitis', '2025-05-17 12:00:00', '2025-05-20 10:00:00'),
(145, 4, 4, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-05-19 09:00:00', '2025-05-20 11:00:00'),
(146, 5, 4, 'Senior dog wellness', 'Comprehensive exam for older dog.', 'Healthy', '2025-05-19 10:00:00', '2025-05-20 12:00:00'),
(147, 6, 4, 'Weight monitoring', 'Concerned about weight loss.', 'Needs higher calorie diet', '2025-05-19 11:00:00', '2025-05-20 13:00:00'),
(148, 1, 4, 'New pet check-up', 'First visit for new puppy.', 'Healthy', '2025-05-19 12:00:00', '2025-05-21 09:00:00'),
(149, 2, 4, 'Lethargy', 'Cat seems tired.', 'Mild viral infection', '2025-05-20 09:00:00', '2025-05-21 10:00:00'),
(150, 3, 4, 'Post-dental check', 'Checking on recovery.', 'Recovering well', '2025-05-20 10:00:00', '2025-05-21 11:00:00'),
(151, 4, 4, 'Microchipping', 'Routine implantation.', 'Microchipped', '2025-05-20 11:00:00', '2025-05-21 12:00:00');

-- Doctor 5 (David Jones) - Week 3
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(152, 1, 5, 'Follow-up chronic pain', 'Dog seems more comfortable.', 'Medication effective', '2025-05-14 11:00:00', '2025-05-15 09:30:00'),
(153, 2, 5, 'Dental cleaning', 'Performing procedure.', 'Successful cleaning', '2025-05-14 12:00:00', '2025-05-15 10:30:00'),
(154, 3, 5, 'Blood test', 'Routine senior blood panel.', 'Normal', '2025-05-14 13:00:00', '2025-05-15 11:30:00'),
(155, 4, 5, 'Vomiting and diarrhea', 'Acute onset.', 'Gastroenteritis', '2025-05-15 09:00:00', '2025-05-15 12:30:00'),
(156, 5, 5, 'Respiratory follow-up', 'Breathing normal.', 'Stable', '2025-05-15 10:00:00', '2025-05-16 10:00:00'),
(157, 6, 5, 'GI stasis recheck', 'Rabbit eating well now.', 'Recovered', '2025-05-15 11:00:00', '2025-05-16 11:00:00'),
(158, 1, 5, 'X-ray for lameness', 'Investigating cause of limp.', 'Mild arthritis', '2025-05-15 12:00:00', '2025-05-16 12:00:00'),
(159, 2, 5, 'Post-dental check', 'Checking recovery.', 'Recovering well', '2025-05-16 09:00:00', '2025-05-17 09:30:00'),
(160, 3, 5, 'Urinary incontinence recheck', 'Medication seems effective.', 'Improvement', '2025-05-16 10:00:00', '2025-05-17 10:30:00'),
(161, 4, 5, 'Annual check-up', 'Routine check-up.', 'Healthy', '2025-05-16 11:00:00', '2025-05-17 11:30:00'),
(162, 5, 5, 'Cardiac consult', 'New heart murmur detected.', 'Referred to cardiologist', '2025-05-16 12:00:00', '2025-05-19 09:30:00'),
(163, 6, 5, 'Diarrhea', 'Acute onset of diarrhea.', 'Dietary change', '2025-05-17 09:00:00', '2025-05-19 10:30:00'),
(164, 1, 5, 'Allergy symptoms', 'Itching, red skin.', 'Food allergy suspected', '2025-05-17 10:00:00', '2025-05-19 11:30:00'),
(165, 2, 5, 'Weight management', 'Reviewing diet and exercise.', 'Maintaining healthy weight', '2025-05-17 11:00:00', '2025-05-20 09:30:00'),
(166, 3, 5, 'Chronic pain re-evaluation', 'Adjusting medication.', 'More comfortable', '2025-05-17 12:00:00', '2025-05-20 10:30:00'),
(167, 4, 5, 'Dental exam', 'Routine check.', 'Needs cleaning soon', '2025-05-19 09:00:00', '2025-05-20 11:30:00'),
(168, 5, 5, 'Behavioral issue', 'Separation anxiety.', 'Recommended training', '2025-05-19 10:00:00', '2025-05-20 12:30:00'),
(169, 6, 5, 'Respiratory infection', 'Coughing and sneezing.', 'Viral infection', '2025-05-19 11:00:00', '2025-05-20 13:30:00'),
(170, 1, 5, 'Follow-up diarrhea', 'Stools normalized.', 'Recovered', '2025-05-19 12:00:00', '2025-05-21 09:30:00'),
(171, 2, 5, 'Annual vaccination', 'Booster shot.', 'Vaccinated', '2025-05-20 09:00:00', '2025-05-21 10:30:00'),
(172, 3, 5, 'Allergy testing', 'Preparing for test.', 'Scheduled for test', '2025-05-20 10:00:00', '2025-05-21 11:30:00'),
(173, 4, 5, 'Acute lameness', 'Sudden limp on hind leg.', 'Sprain', '2025-05-20 11:00:00', '2025-05-21 12:30:00');

-- Doctor 8 (Olivia Brown) - Week 3
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(174, 1, 8, 'Atopic dermatitis review', 'Checking medication effectiveness.', 'Stable', '2025-05-14 12:00:00', '2025-05-15 10:00:00'),
(175, 2, 8, 'Skin infection recheck', 'Lesions healing.', 'Healing well', '2025-05-14 13:00:00', '2025-05-15 11:00:00'),
(176, 3, 8, 'Alopecia investigation', 'Patches of hair loss.', 'Hormonal imbalance suspected', '2025-05-14 14:00:00', '2025-05-15 12:00:00'),
(177, 4, 8, 'Ear mites recheck', 'No more scratching.', 'Clear', '2025-05-15 09:00:00', '2025-05-15 13:00:00'),
(178, 5, 8, 'Persistent itching', 'Still scratching excessively.', 'Needs deeper investigation', '2025-05-15 10:00:00', '2025-05-16 10:30:00'),
(179, 6, 8, 'Fungal infection follow-up', 'Skin clearing up.', 'Improving', '2025-05-15 11:00:00', '2025-05-16 11:30:00'),
(180, 1, 8, 'New skin growth', 'Small bump on paw.', 'Benign cyst', '2025-05-15 12:00:00', '2025-05-16 12:30:00'),
(181, 2, 8, 'Dandruff management', 'Discussing dietary supplements.', 'Recommended fish oil', '2025-05-16 09:00:00', '2025-05-17 10:00:00'),
(182, 3, 8, 'Skin allergy testing', 'Conducting patch tests.', 'Results in a few days', '2025-05-16 10:00:00', '2025-05-17 11:00:00'),
(183, 4, 8, 'Pustules on skin', 'New flare-up.', 'Bacterial infection', '2025-05-16 11:00:00', '2025-05-17 12:00:00'),
(184, 5, 8, 'Chronic ear infection management', 'Reviewing long-term strategy.', 'Ongoing management', '2025-05-16 12:00:00', '2025-05-19 10:00:00'),
(185, 6, 8, 'Foot pad lesion', 'Not healing well.', 'Needs surgical debridement', '2025-05-17 09:00:00', '2025-05-19 11:00:00'),
(186, 1, 8, 'Flea allergy dermatitis', 'Severe itching.', 'Treated for fleas', '2025-05-17 10:00:00', '2025-05-19 12:00:00'),
(187, 2, 8, 'Skin cytology', 'Examining skin cells under microscope.', 'Yeast overgrowth', '2025-05-17 11:00:00', '2025-05-20 10:00:00'),
(188, 3, 8, 'Alopecia recheck', 'Hair starting to grow back.', 'Improving', '2025-05-17 12:00:00', '2025-05-20 11:00:00'),
(189, 4, 8, 'Environmental allergies', 'Itching increases seasonally.', 'Started immunotherapy', '2025-05-19 09:00:00', '2025-05-20 12:00:00'),
(190, 5, 8, 'Skin tumor consult', 'Discussing biopsy results.', 'Benign', '2025-05-19 10:00:00', '2025-05-20 13:00:00'),
(191, 6, 8, 'Post-op skin lesion', 'Checking surgical site.', 'Healing well', '2025-05-19 11:00:00', '2025-05-20 14:00:00'),
(192, 1, 8, 'New client consult - skin issue', 'First visit for a dog with chronic ear issues.', 'Chronic otitis', '2025-05-19 12:00:00', '2025-05-21 10:00:00'),
(193, 2, 8, 'Yeast infection recheck', 'Skin less red.', 'Clearing', '2025-05-20 09:00:00', '2025-05-21 11:00:00'),
(194, 3, 8, 'Skin maintenance for senior dog', 'Preventative care.', 'Healthy', '2025-05-20 10:00:00', '2025-05-21 12:00:00'),
(195, 4, 8, 'Pruritus investigation', 'Generalized itching.', 'Allergic reaction', '2025-05-20 11:00:00', '2025-05-21 13:00:00');

-- Week 4 (May 22nd - May 24th) - Current date is May 24, 2025, so appointments up to now.
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
-- Doctor 4 - Week 4
(196, 1, 4, 'Annual check-up', 'Routine check-up for dog.', 'Healthy', '2025-05-21 09:00:00', '2025-05-22 09:00:00'),
(197, 2, 4, 'Lethargy recheck', 'Cat more active.', 'Recovering', '2025-05-21 10:00:00', '2025-05-22 10:00:00'),
(198, 3, 4, 'Weight loss consultation', 'Dog losing weight unintentionally.', 'Needs dietary change', '2025-05-21 11:00:00', '2025-05-22 11:00:00'),
(199, 4, 4, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-05-22 09:00:00', '2025-05-22 12:00:00'),
(200, 5, 4, 'Dental cleaning', 'Performing procedure.', 'Successful cleaning', '2025-05-22 10:00:00', '2025-05-23 09:00:00'),
(201, 6, 4, 'Respiratory follow-up', 'Rabbit breathing better.', 'Improving', '2025-05-22 11:00:00', '2025-05-23 10:00:00'),
(202, 1, 4, 'Post-dental check', 'Checking recovery.', 'Recovering well', '2025-05-22 12:00:00', '2025-05-23 11:00:00'),
(203, 2, 4, 'Ear infection recheck', 'Ears clearing.', 'Clearing', '2025-05-23 09:00:00', '2025-05-23 12:00:00'),
(204, 3, 4, 'Routine check-up', 'Annual visit.', 'Healthy', '2025-05-23 10:00:00', '2025-05-24 09:00:00'),
(205, 4, 4, 'Limping recheck', 'Cat still slightly limping.', 'Needs further observation', '2025-05-23 11:00:00', '2025-05-24 10:00:00'),
(206, 5, 4, 'Behavioral issue', 'Excessive barking.', 'Recommended training', '2025-05-23 12:00:00', '2025-05-24 11:00:00'),
(207, 6, 4, 'Weight check', 'Monitoring rabbit weight.', 'Stable', '2025-05-24 09:00:00', '2025-05-24 12:00:00');

-- Doctor 5 (David Jones) - Week 4
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(208, 1, 5, 'Arthritis recheck', 'Dog more comfortable.', 'Medication effective', '2025-05-21 10:00:00', '2025-05-22 09:30:00'),
(209, 2, 5, 'Urinary issue recheck', 'Symptoms resolved.', 'Cleared', '2025-05-21 11:00:00', '2025-05-22 10:30:00'),
(210, 3, 5, 'Chronic cough follow-up', 'Cough less frequent.', 'Improving', '2025-05-21 12:00:00', '2025-05-22 11:30:00'),
(211, 4, 5, 'Emergency consult', 'Cat ingested something toxic.', 'Inducing vomiting', '2025-05-22 09:00:00', '2025-05-22 12:30:00'),
(212, 5, 5, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-05-22 10:00:00', '2025-05-23 10:00:00'),
(213, 6, 5, 'Dental exam', 'Routine check.', 'Healthy teeth', '2025-05-22 11:00:00', '2025-05-23 11:00:00'),
(214, 1, 5, 'Follow-up toxic ingestion', 'Dog recovered well.', 'Stable', '2025-05-22 12:00:00', '2025-05-23 12:00:00'),
(215, 2, 5, 'Routine check-up', 'Annual visit.', 'Healthy', '2025-05-23 09:00:00', '2025-05-23 13:00:00'),
(216, 3, 5, 'Kidney disease recheck', 'Monitoring function.', 'Stable', '2025-05-23 10:00:00', '2025-05-24 09:30:00'),
(217, 4, 5, 'Allergy medication review', 'Discussing current medication.', 'Adjusted dosage', '2025-05-23 11:00:00', '2025-05-24 10:30:00'),
(218, 5, 5, 'Senior check-up', 'Comprehensive exam for older dog.', 'Normal for age', '2025-05-23 12:00:00', '2025-05-24 11:30:00'),
(219, 6, 5, 'Behavioral issue', 'Rabbit gnawing on cage.', 'Needs more toys', '2025-05-24 09:00:00', '2025-05-24 12:30:00');

-- Doctor 8 (Olivia Brown) - Week 4
INSERT INTO `Appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`) VALUES
(220, 1, 8, 'Skin cytology recheck', 'Yeast count reduced.', 'Improving', '2025-05-21 11:00:00', '2025-05-22 10:00:00'),
(221, 2, 8, 'Chronic itching follow-up', 'Still scratching, but less severe.', 'Monitoring', '2025-05-21 12:00:00', '2025-05-22 11:00:00'),
(222, 3, 8, 'Alopecia follow-up', 'Hair growing back well.', 'Recovering', '2025-05-21 13:00:00', '2025-05-22 12:00:00'),
(223, 4, 8, 'Fungal infection recheck', 'Skin clear.', 'Cleared', '2025-05-22 10:00:00', '2025-05-22 13:00:00'),
(224, 5, 8, 'Allergy injection', 'Routine immunotherapy shot.', 'Administered', '2025-05-22 11:00:00', '2025-05-23 10:30:00'),
(225, 6, 8, 'Foot pad lesion recheck', 'Wound healing well.', 'Healing', '2025-05-22 12:00:00', '2025-05-23 11:30:00'),
(226, 1, 8, 'New client consult - skin issue', 'First visit for dog with chronic ear issues.', 'Chronic otitis', '2025-05-22 13:00:00', '2025-05-23 12:30:00'),
(227, 2, 8, 'Dermatitis follow-up', 'Skin calming.', 'Improving', '2025-05-23 10:00:00', '2025-05-23 13:30:00'),
(228, 3, 8, 'Skin mass consult', 'Discussing removal options.', 'Scheduled for surgery', '2025-05-23 11:00:00', '2025-05-24 10:00:00'),
(229, 4, 8, 'Flea allergy dermatitis', 'Still some itching.', 'Adjusted flea control', '2025-05-23 12:00:00', '2025-05-24 11:00:00'),
(230, 5, 8, 'General skin health advice', 'Owner seeking preventative tips.', 'Advised on diet and grooming', '2025-05-23 13:00:00', '2025-05-24 12:00:00'),
(231, 6, 8, 'Pruritus investigation', 'Generalized itching.', 'Environmental allergy', '2025-05-24 09:00:00', '2025-05-24 13:00:00');

/*!40000 ALTER TABLE `Appointment` ENABLE KEYS */;
UNLOCK TABLES;


-- Insert data into `AppointmentServices` table (continue IDs from 4)
LOCK TABLES `AppointmentServices` WRITE;
/*!40000 ALTER TABLE `AppointmentServices` DISABLE KEYS */;
INSERT INTO `AppointmentServices` (`id`, `appointment_id`, `service_id`) VALUES
(1, 1, 1),
(2, 2, 3),
(3, 3, 1),
(4, 4, 5), -- Vaccination booster
(5, 5, 1), -- Flea treatment
(6, 6, 1), -- Ear infection check
(7, 7, 1), -- Routine check-up
(8, 8, 1), -- Nail trim
(9, 9, 2), -- Dental check
(10, 10, 1), -- Follow-up ear infection
(11, 11, 1), -- Weight management consultation
(12, 12, 5), -- Annual vaccinations
(13, 13, 3), -- Lethargy investigation
(14, 14, 1), -- Deworming
(15, 15, 3), -- Diarrhea
(16, 16, 3), -- Skin rash
(17, 17, 3), -- Eye discharge
(18, 18, 6), -- Check on limping (X-ray follow-up)
(19, 19, 3), -- Appetite loss
(20, 20, 1), -- Regular check-up
(21, 21, 1), -- Routine check-up (Rabbit)
(22, 22, 1), -- Allergy medication refill (Check-up for refill)
(23, 23, 1), -- Recheck eye infection

(24, 24, 3), -- Digestive upset
(25, 25, 3), -- Urinary issues
(26, 26, 3), -- Chronic cough
(27, 27, 3), -- Sudden lameness
(28, 28, 3), -- Skin irritation
(29, 29, 3), -- Loss of balance
(30, 30, 1), -- Recheck gastroenteritis
(31, 31, 7), -- UTI follow-up (Blood Test implies diagnostics)
(32, 32, 1), -- Kennel cough follow-up
(33, 33, 1), -- Annual check-up
(34, 34, 1), -- Behavioral consultation
(35, 35, 3), -- Eye injury
(36, 36, 5), -- Routine vaccination
(37, 37, 2), -- Dental check-up
(38, 38, 1), -- Grooming advise
(39, 39, 3), -- Fever
(40, 40, 1), -- Arthritis management
(41, 41, 1), -- Follow-up eye injury
(42, 42, 1), -- Annual check-up
(43, 43, 1), -- Weight loss program

(44, 44, 8), -- Allergy flare-up
(45, 45, 1), -- Hot spots
(46, 46, 1), -- Skin growth
(47, 47, 1), -- Excessive shedding
(48, 48, 1), -- Pustules on skin
(49, 49, 1), -- Mite infestation
(50, 50, 1), -- Atopic dermatitis follow-up
(51, 51, 1), -- Hot spots recheck
(52, 52, 1), -- Annual skin check
(53, 53, 8), -- Allergy testing
(54, 54, 1), -- Skin infection follow-up
(55, 55, 1), -- Hair loss patches
(56, 56, 1), -- Food allergy consultation
(57, 57, 1), -- Chronic itching
(58, 58, 1), -- Dull coat
(59, 59, 1), -- Itchy paws
(60, 60, 1), -- Skin biopsy results (consultation)
(61, 61, 1), -- Fungal infection recheck
(62, 62, 1), -- Eczema management
(63, 63, 1), -- Follow-up chronic itching

(64, 64, 2), -- Dental Cleaning Prep
(65, 65, 1), -- Post-op check
(66, 66, 5), -- Annual vaccinations
(67, 67, 4), -- Lump removal consult (surgery)
(68, 68, 3), -- Coughing
(69, 69, 1), -- Wellness exam
(70, 70, 2), -- Dental Cleaning
(71, 71, 1), -- Ear cleaning
(72, 72, 1), -- Puppy check-up
(73, 73, 1), -- Follow-up diarrhea
(74, 74, 3), -- Gastrointestinal upset
(75, 75, 1), -- Skin lump check
(76, 76, 1), -- Post-dental check
(77, 77, 5), -- Vaccination
(78, 78, 3), -- Limping - acute
(79, 79, 3), -- Respiratory symptoms
(80, 80, 1), -- Follow-up cough
(81, 81, 1), -- Microchipping
(82, 82, 1), -- New pet check-up
(83, 83, 1), -- Recheck lameness
(84, 84, 1), -- Senior check-up
(85, 85, 1), -- Weight check

(86, 86, 7), -- Blood test results (consultation)
(87, 87, 1), -- UTI recheck
(88, 88, 1), -- Lethargy in older dog
(89, 89, 3), -- Chronic vomiting
(90, 90, 3), -- Respiratory distress
(91, 91, 3), -- Loss of appetite - rabbit
(92, 92, 1), -- Wellness check
(93, 93, 2), -- Dental scaling consult
(94, 94, 1), -- Kidney disease management
(95, 95, 1), -- Recheck chronic vomiting
(96, 96, 1), -- Asthma follow-up
(97, 97, 1), -- GI stasis follow-up
(98, 98, 3), -- Urinary incontinence
(99, 99, 3), -- Eye infection
(100, 100, 1), -- Heart murmur recheck
(101, 101, 1), -- Annual check-up
(102, 102, 1), -- Allergy medication review
(103, 103, 2), -- Dental exam
(104, 104, 1), -- Pain management consult
(105, 105, 1), -- Recheck eye infection
(106, 106, 1), -- Dietary advice
(107, 107, 1), -- Behavioral issue

(108, 108, 8), -- Allergy retest
(109, 109, 1), -- Dermatitis follow-up
(110, 110, 1), -- Chronic itching
(111, 111, 1), -- Skin infection
(112, 112, 1), -- Hair loss patches
(113, 113, 1), -- Excessive grooming
(114, 114, 1), -- Rash on belly
(115, 115, 1), -- Skin scraping (diagnostic)
(116, 116, 8), -- Food allergy test
(117, 117, 1), -- Dandruff and dry skin
(118, 118, 1), -- Ringworm recheck
(119, 119, 1), -- Behavioral grooming follow-up
(120, 120, 1), -- Recheck contact dermatitis
(121, 121, 1), -- Allergy medication adjustment
(122, 122, 1), -- Chronic ear infections
(123, 123, 1), -- Foot pad irritation
(124, 124, 1), -- Annual skin check
(125, 125, 1), -- New skin tags
(126, 126, 1), -- Folliculitis
(127, 127, 1), -- Suspected flea allergy
(128, 128, 1), -- General skin health advice
(129, 129, 1), -- Ulcerated skin lesion

(130, 130, 2), -- Dental cleaning consult
(131, 131, 1), -- Weight check
(132, 132, 1), -- Ear cleaning
(133, 133, 1), -- New pet check-up
(134, 134, 1), -- Post-op check
(135, 135, 5), -- Vaccination
(136, 136, 1), -- Follow-up GI upset
(137, 137, 1), -- Annual check-up
(138, 138, 1), -- Lump check
(139, 139, 1), -- Behavioral consultation
(140, 140, 1), -- Recheck limping
(141, 141, 3), -- Snuffles symptoms
(142, 142, 1), -- Allergy medication refill
(143, 143, 2), -- Routine dental check
(144, 144, 3), -- Ear infection
(145, 145, 5), -- Vaccination
(146, 146, 1), -- Senior dog wellness
(147, 147, 1), -- Weight monitoring
(148, 148, 1), -- New pet check-up
(149, 149, 3), -- Lethargy
(150, 150, 1), -- Post-dental check
(151, 151, 1), -- Microchipping

(152, 152, 1), -- Follow-up chronic pain
(153, 153, 2), -- Dental cleaning
(154, 154, 7), -- Blood test
(155, 155, 3), -- Vomiting and diarrhea
(156, 156, 1), -- Respiratory follow-up
(157, 157, 1), -- GI stasis recheck
(158, 158, 6), -- X-ray for lameness
(159, 159, 1), -- Post-dental check
(160, 160, 1), -- Urinary incontinence recheck
(161, 161, 1), -- Annual check-up
(162, 162, 1), -- Cardiac consult
(163, 163, 3), -- Diarrhea
(164, 164, 3), -- Allergy symptoms
(165, 165, 1), -- Weight management
(166, 166, 1), -- Chronic pain re-evaluation
(167, 167, 2), -- Dental exam
(168, 168, 1), -- Behavioral issue
(169, 169, 3), -- Respiratory infection
(170, 170, 1), -- Follow-up diarrhea
(171, 171, 5), -- Annual vaccination
(172, 172, 8), -- Allergy testing
(173, 173, 3), -- Acute lameness

(174, 174, 1), -- Atopic dermatitis review
(175, 175, 1), -- Skin infection recheck
(176, 176, 1), -- Alopecia investigation
(177, 177, 1), -- Ear mites recheck
(178, 178, 1), -- Persistent itching
(179, 179, 1), -- Fungal infection follow-up
(180, 180, 1), -- New skin growth
(181, 181, 1), -- Dandruff management
(182, 182, 8), -- Skin allergy testing
(183, 183, 1), -- Pustules on skin
(184, 184, 1), -- Chronic ear infection management
(185, 185, 4), -- Foot pad lesion (surgery)
(186, 186, 1), -- Flea allergy dermatitis
(187, 187, 1), -- Skin cytology (diagnostic)
(188, 188, 1), -- Alopecia recheck
(189, 189, 1), -- Environmental allergies
(190, 190, 1), -- Skin tumor consult
(191, 191, 1), -- Post-op skin lesion
(192, 192, 1), -- New client consult - skin issue
(193, 193, 1), -- Yeast infection recheck
(194, 194, 1), -- Skin maintenance for senior dog
(195, 195, 1), -- Pruritus investigation

(196, 196, 1), -- Annual check-up
(197, 197, 1), -- Lethargy recheck
(198, 198, 1), -- Weight loss consultation
(199, 199, 5), -- Vaccination
(200, 200, 2), -- Dental cleaning
(201, 201, 1), -- Respiratory follow-up
(202, 202, 1), -- Post-dental check
(203, 203, 1), -- Ear infection recheck
(204, 204, 1), -- Routine check-up
(205, 205, 1), -- Limping recheck
(206, 206, 1), -- Behavioral issue
(207, 207, 1), -- Weight check

(208, 208, 1), -- Arthritis recheck
(209, 209, 1), -- Urinary issue recheck
(210, 210, 1), -- Chronic cough follow-up
(211, 211, 3), -- Emergency consult
(212, 212, 5), -- Vaccination
(213, 213, 2), -- Dental exam
(214, 214, 1), -- Follow-up toxic ingestion
(215, 215, 1), -- Routine check-up
(216, 216, 1), -- Kidney disease recheck
(217, 217, 1), -- Allergy medication review
(218, 218, 1), -- Senior check-up
(219, 219, 1), -- Behavioral issue

(220, 220, 1), -- Skin cytology recheck
(221, 221, 1), -- Chronic itching follow-up
(222, 222, 1), -- Alopecia follow-up
(223, 223, 1), -- Fungal infection recheck
(224, 224, 1), -- Allergy injection
(225, 225, 1), -- Foot pad lesion recheck
(226, 226, 1), -- New client consult - skin issue
(227, 227, 1), -- Dermatitis follow-up
(228, 228, 4), -- Skin mass consult (surgery)
(229, 229, 1), -- Flea allergy dermatitis
(230, 230, 1), -- General skin health advice
(231, 231, 1); -- Pruritus investigation

/*!40000 ALTER TABLE `AppointmentServices` ENABLE KEYS */;
UNLOCK TABLES;

-- Insert data into `Drug` table
INSERT INTO `Drug` (`id`, `name`, `dosage_form`, `strength`, `unit_of_measure`, `description`, `manufacturer`, `stock_quantity`) VALUES
(1, 'Amoxicillin', 'Tablet', '250mg', 'mg', 'Broad-spectrum antibiotic', 'PharmaCo', 500),
(2, 'Meloxicam', 'Liquid', '1.5mg/ml', 'ml', 'NSAID for pain and inflammation', 'VetMed Labs', 200),
(3, 'Flea & Tick Prevention', 'Tablet', '1 tablet', 'tablet', 'Monthly preventative treatment', 'PetHealth Inc.', 100),
(4, 'Hydrocortisone Spray', 'Liquid', '1%', 'ml', 'Topical steroid for skin irritation', 'DermVet Pharma', 150),
(5, 'Gabapentin', 'Capsule', '100mg', 'mg', 'Pain relief and anti-anxiety', 'NeuroVet', 300),
(6, 'Probiotic', 'Powder', '5 billion CFU', 'sachet', 'Supports digestive health', 'GutHealth Co.', 250);


-- Insert data into `Prescription` table (continue IDs from 3)
LOCK TABLES `Prescription` WRITE;
/*!40000 ALTER TABLE `Prescription` DISABLE KEYS */;
INSERT INTO `Prescription` (`id`, `appointment_id`, `instructions`, `expiry_date`, `created_at`) VALUES
(1, 2, 'Administer with food twice daily for 7 days.', '2025-06-10 00:00:00', '2025-05-23 15:00:00'),
(2, 3, 'Give orally once daily for 5 days. Re-evaluate after X-ray results.', '2025-06-15 00:00:00', '2025-05-24 12:00:00'),
(3, 6, 'Administer 1 tablet orally twice daily for 10 days.', '2025-06-15 00:00:00', '2025-05-01 11:30:00'),
(4, 24, 'Administer 2ml orally twice daily for 5 days.', '2025-06-10 00:00:00', '2025-05-01 10:00:00'),
(5, 25, 'Administer 1 tablet orally once daily for 7 days.', '2025-06-12 00:00:00', '2025-05-01 11:00:00'),
(6, 44, 'Spray affected area twice daily.', '2025-06-15 00:00:00', '2025-05-01 10:30:00'),
(7, 45, 'Apply cream to hot spots daily.', '2025-06-16 00:00:00', '2025-05-01 11:30:00'),
(8, 16, 'Administer 1ml orally once daily for 3 days.', '2025-06-08 00:00:00', '2025-05-06 09:30:00'),
(9, 28, 'Apply spray twice daily to affected areas for 7 days.', '2025-06-10 00:00:00', '2025-05-02 11:30:00'),
(10, 48, 'Apply topical antibiotic twice daily for 10 days.', '2025-06-18 00:00:00', '2025-05-02 12:00:00'),
(11, 55, 'Apply antifungal cream twice daily for 14 days.', '2025-06-19 00:00:00', '2025-05-05 12:30:00'),
(12, 68, 'Administer 0.5ml orally twice daily for 7 days.', '2025-06-20 00:00:00', '2025-05-09 11:00:00'),
(13, 79, 'Administer 0.2ml orally once daily for 5 days.', '2025-06-21 00:00:00', '2025-05-13 12:30:00'),
(14, 90, 'Administer 1 tablet orally twice daily for 10 days.', '2025-06-22 00:00:00', '2025-05-09 11:30:00'),
(15, 111, 'Apply topical antibiotic twice daily for 7 days.', '2025-06-23 00:00:00', '2025-05-09 11:00:00'),
(16, 112, 'Administer 1 tablet orally once daily for 14 days.', '2025-06-24 00:00:00', '2025-05-09 12:00:00'),
(17, 141, 'Administer 0.3ml orally twice daily for 7 days.', '2025-07-01 00:00:00', '2025-05-19 10:30:00'),
(18, 144, 'Apply ear drops twice daily for 7 days.', '2025-07-02 00:00:00', '2025-05-20 10:30:00'),
(19, 155, 'Administer 1 sachet orally once daily for 5 days.', '2025-07-03 00:00:00', '2025-05-15 13:00:00'),
(20, 169, 'Administer 0.4ml orally twice daily for 7 days.', '2025-07-04 00:00:00', '2025-05-20 14:00:00'),
(21, 183, 'Apply topical antibiotic twice daily for 7 days.', '2025-07-05 00:00:00', '2025-05-17 12:30:00'),
(22, 187, 'Administer 1 sachet orally once daily for 10 days.', '2025-07-06 00:00:00', '2025-05-20 10:30:00'),
(23, 211, 'Administer 1 tablet orally once.', '2025-07-07 00:00:00', '2025-05-22 13:00:00');

/*!40000 ALTER TABLE `Prescription` ENABLE KEYS */;
UNLOCK TABLES;

-- Insert data into `PrescriptionDrugs` table (continue IDs from 3)
LOCK TABLES `PrescriptionDrugs` WRITE;
/*!40000 ALTER TABLE `PrescriptionDrugs` DISABLE KEYS */;
INSERT INTO `PrescriptionDrugs` (`id`, `prescription_id`, `drug_id`, `quantity`, `dosage_instructions`) VALUES
(1, 1, 1, 14, 'One 250mg tablet, twice daily with food.'),
(2, 2, 2, 50, '2 ml orally once daily for 5 days.'),
(3, 3, 1, 20, 'One 250mg tablet, twice daily.'),
(4, 4, 2, 10, '2ml orally twice daily.'),
(5, 5, 1, 7, 'One 250mg tablet, once daily.'),
(6, 6, 4, 1, 'Spray affected area twice daily.'),
(7, 7, 4, 1, 'Apply to hot spots daily.'),
(8, 8, 2, 3, '1ml orally once daily.'),
(9, 9, 4, 1, 'Spray twice daily to affected areas.'),
(10, 10, 1, 1, 'Apply topical antibiotic twice daily.'),
(11, 11, 4, 1, 'Apply antifungal cream twice daily.'),
(12, 12, 2, 3.5, '0.5ml orally twice daily.'),
(13, 13, 2, 1, '0.2ml orally once daily.'),
(14, 14, 5, 20, 'One 100mg capsule twice daily.'),
(15, 15, 1, 1, 'Apply topical antibiotic twice daily.'),
(16, 16, 3, 14, 'One tablet orally once daily.'),
(17, 17, 2, 4.2, '0.3ml orally twice daily.'),
(18, 18, 4, 1, 'Apply ear drops twice daily.'),
(19, 19, 6, 5, 'One sachet orally once daily.'),
(20, 20, 2, 5.6, '0.4ml orally twice daily.'),
(21, 21, 1, 1, 'Apply topical antibiotic twice daily.'),
(22, 22, 6, 10, 'One sachet orally once daily.'),
(23, 23, 1, 1, 'One tablet orally once.');

/*!40000 ALTER TABLE `PrescriptionDrugs` ENABLE KEYS */;
UNLOCK TABLES;

-- Insert data into `Opinion` table (continue IDs from 4)
LOCK TABLES `Opinion` WRITE;
/*!40000 ALTER TABLE `Opinion` DISABLE KEYS */;
INSERT INTO `Opinion` (`id`, `client_id`, `doctor_id`, `comment`, `rating`, `created_at`) VALUES
(1, 2, 4, 'Dr. Smith was excellent, very thorough with Burek.', 5, '2025-05-23 11:00:00'),
(2, 3, 4, 'Azor received great care. Very professional and friendly.', 4, '2025-05-25 10:00:00'),
(3, 2, 5, 'Dr. Jones was helpful but the cat is still recovering.', 3, '2025-05-24 16:00:00'),
(4, 6, 4, 'Dr. Smith was fantastic with Kropka\'s check-up. Highly recommend!', 5, '2025-05-02 10:30:00'),
(5, 7, 5, 'Dr. Jones helped Max with his allergies. Very knowledgeable.', 4, '2025-05-02 11:30:00'),
(6, 2, 8, 'Dr. Brown quickly diagnosed Burek\'s skin issue. Great service.', 5, '2025-05-01 10:30:00'),
(7, 3, 4, 'Excellent dental cleaning for Azor. So grateful!', 5, '2025-05-10 10:30:00'),
(8, 6, 5, 'Dr. Jones was patient with Rysiek\'s GI issues. Good follow-up.', 4, '2025-05-09 12:30:00'),
(9, 7, 8, 'Olivia Brown is the best dermatologist! Max\'s skin is much better.', 5, '2025-05-05 11:30:00'),
(10, 2, 4, 'Another smooth vaccination visit with Dr. Smith.', 4, '2025-05-07 10:30:00'),
(11, 3, 5, 'Dr. Jones identified Azor\'s UTI quickly. Professional and caring.', 5, '2025-05-03 11:00:00'),
(12, 6, 8, 'Rysiek\'s fungal infection is clearing up, thanks to Dr. Brown.', 4, '2025-05-07 15:30:00'),
(13, 7, 4, 'Max\'s post-op recovery is going well. Dr. Smith is very attentive.', 5, '2025-05-08 10:30:00'),
(14, 2, 5, 'Burek\'s chronic pain is much better managed now. Thank you, Dr. Jones.', 5, '2025-05-15 10:00:00'),
(15, 3, 8, 'Dr. Brown is thorough with skin checks. Azor had a minor growth removed.', 4, '2025-05-15 12:30:00'),
(16, 6, 4, 'Kropka\'s annual check-up was quick and efficient.', 4, '2025-05-17 11:30:00'),
(17, 7, 5, 'Dr. Jones handled Max\'s emergency consultation very well.', 5, '2025-05-22 13:00:00'),
(18, 2, 8, 'Burek\'s chronic ear issues are finally under control thanks to Dr. Brown.', 5, '2025-05-21 10:30:00'),
(19, 3, 4, 'Azor\'s diet change was recommended by Dr. Smith and is helping with his weight.', 4, '2025-05-22 11:30:00'),
(20, 6, 5, 'Rysiek needed more toys for his behavior, good advice from Dr. Jones.', 3, '2025-05-24 13:00:00'),
(21, 7, 8, 'Olivia Brown is incredibly detailed in her skin assessments. Max is in good hands.', 5, '2025-05-24 12:30:00');

/*!40000 ALTER TABLE `Opinion` ENABLE KEYS */;
UNLOCK TABLES;