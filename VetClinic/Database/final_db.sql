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
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `gender` enum('Female','Male') NOT NULL,
  `role` enum('client','admin') NOT NULL DEFAULT 'client',
  `telephone_number` int NOT NULL,
  `date_of_birth` datetime NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `last_login` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Doctors`
--

DROP TABLE IF EXISTS `Doctors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Doctors` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `gender` enum('Female','Male') NOT NULL,
  `telephone_number` int NOT NULL,
  `date_of_birth` datetime NOT NULL,
  `specialization` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `last_login` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Doctors`
--

LOCK TABLES `Doctors` WRITE;
/*!40000 ALTER TABLE `Doctors` DISABLE KEYS */;
/*!40000 ALTER TABLE `Doctors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Drugs`
--

DROP TABLE IF EXISTS `Drugs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Drugs` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `dosage_form` varchar(255) NOT NULL,
  `strength` varchar(255) NOT NULL,
  `unit_of_measure` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `manufacturer` varchar(255) NOT NULL,
  `stock_quantity` bigint NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Drugs`
--

LOCK TABLES `Drugs` WRITE;
/*!40000 ALTER TABLE `Drugs` DISABLE KEYS */;
/*!40000 ALTER TABLE `Drugs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Services`
--

DROP TABLE IF EXISTS `Services`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Services` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `price` decimal(10,0) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Services`
--

LOCK TABLES `Services` WRITE;
/*!40000 ALTER TABLE `Services` DISABLE KEYS */;
/*!40000 ALTER TABLE `Services` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Pets`
--

DROP TABLE IF EXISTS `Pets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Pets` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  `breed` varchar(255) NOT NULL,
  `species` varchar(255) NOT NULL,
  `weight` double NOT NULL,
  `gender` enum('Male','Female','Neutered','Spayed') NOT NULL,
  `date_of_birth` datetime NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_user_pets` (`user_id`),
  CONSTRAINT `fk_user_pets` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Pets`
--

LOCK TABLES `Pets` WRITE;
/*!40000 ALTER TABLE `Pets` DISABLE KEYS */;
/*!40000 ALTER TABLE `Pets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Appointments`
--

DROP TABLE IF EXISTS `Appointments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Appointments` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pet_id` int NOT NULL,
  `doctor_id` int NOT NULL,
  `status` enum('Sheduled','In Progress','Completed','Canceled') NOT NULL DEFAULT 'Sheduled',
  `reason_for_visit` varchar(255) NOT NULL,
  `diagnosis` varchar(255) NOT NULL,
  `notes` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `appointment_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_pet_appointments` (`pet_id`),
  KEY `fk_doctor_appointments` (`doctor_id`),
  CONSTRAINT `fk_doctor_appointments` FOREIGN KEY (`doctor_id`) REFERENCES `Doctors` (`id`),
  CONSTRAINT `fk_pet_appointments` FOREIGN KEY (`pet_id`) REFERENCES `Pets` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Appointments`
--

LOCK TABLES `Appointments` WRITE;
/*!40000 ALTER TABLE `Appointments` DISABLE KEYS */;
/*!40000 ALTER TABLE `Appointments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Opinions`
--

DROP TABLE IF EXISTS `Opinions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Opinions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `doctor_id` int NOT NULL,
  `comment` varchar(255) NOT NULL,
  `rating` smallint NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_user_comments` (`user_id`),
  KEY `fk_doctor_comments` (`doctor_id`),
  CONSTRAINT `fk_doctor_comments` FOREIGN KEY (`doctor_id`) REFERENCES `Doctors` (`id`),
  CONSTRAINT `fk_user_comments` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Opinions`
--

LOCK TABLES `Opinions` WRITE;
/*!40000 ALTER TABLE `Opinions` DISABLE KEYS */;
/*!40000 ALTER TABLE `Opinions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Prescriptions`
--

DROP TABLE IF EXISTS `Prescriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Prescriptions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `appointment_id` int NOT NULL,
  `expiry_date` datetime NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_prescription_appointment` (`appointment_id`),
  CONSTRAINT `fk_prescription_appointment` FOREIGN KEY (`appointment_id`) REFERENCES `Appointments` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Prescriptions`
--

LOCK TABLES `Prescriptions` WRITE;
/*!40000 ALTER TABLE `Prescriptions` DISABLE KEYS */;
/*!40000 ALTER TABLE `Prescriptions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AppointmentServices`
--

DROP TABLE IF EXISTS `AppointmentServices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AppointmentServices` (
  `appointment_id` int NOT NULL,
  `service_id` int NOT NULL,
  KEY `fk_appointment_app_ser` (`appointment_id`),
  KEY `fk_service_app_ser` (`service_id`),
  CONSTRAINT `fk_appointment_app_ser` FOREIGN KEY (`appointment_id`) REFERENCES `Appointments` (`id`),
  CONSTRAINT `fk_service_app_ser` FOREIGN KEY (`service_id`) REFERENCES `Services` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppointmentServices`
--

LOCK TABLES `AppointmentServices` WRITE;
/*!40000 ALTER TABLE `AppointmentServices` DISABLE KEYS */;
/*!40000 ALTER TABLE `AppointmentServices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PrescriptionDrugs`
--

DROP TABLE IF EXISTS `PrescriptionDrugs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PrescriptionDrugs` (
  `prescription_id` int NOT NULL,
  `drug_id` int NOT NULL,
  `quantity` int NOT NULL,
  `dosage_instructions` varchar(255) NOT NULL,
  KEY `fk_prescription_pre_drug` (`prescription_id`),
  KEY `fk_drug_pre_drug` (`drug_id`),
  CONSTRAINT `fk_drug_pre_drug` FOREIGN KEY (`drug_id`) REFERENCES `Drugs` (`id`),
  CONSTRAINT `fk_prescription_pre_drug` FOREIGN KEY (`prescription_id`) REFERENCES `Prescriptions` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PrescriptionDrugs`
--

LOCK TABLES `PrescriptionDrugs` WRITE;
/*!40000 ALTER TABLE `PrescriptionDrugs` DISABLE KEYS */;
/*!40000 ALTER TABLE `PrescriptionDrugs` ENABLE KEYS */;
UNLOCK TABLES;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-13 22:53:04
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-12 14:02:57

-- Disable foreign key checks for easier data insertion
SET FOREIGN_KEY_CHECKS = 0;

-- Truncate tables to ensure a clean slate for seeding
TRUNCATE TABLE `AppointmentServices`;
TRUNCATE TABLE `PrescriptionDrugs`;
TRUNCATE TABLE `Prescriptions`;
TRUNCATE TABLE `Appointments`;
TRUNCATE TABLE `Opinions`;
TRUNCATE TABLE `Pets`;
TRUNCATE TABLE `Doctors`;
TRUNCATE TABLE `Users`;
TRUNCATE TABLE `Services`;
TRUNCATE TABLE `Drugs`;

-- Reset AUTO_INCREMENT for tables with explicit IDs that might conflict
ALTER TABLE `Users` AUTO_INCREMENT = 1;
ALTER TABLE `Doctors` AUTO_INCREMENT = 1;
ALTER TABLE `Pets` AUTO_INCREMENT = 1;
ALTER TABLE `Services` AUTO_INCREMENT = 1;
ALTER TABLE `Drugs` AUTO_INCREMENT = 1;
ALTER TABLE `Appointments` AUTO_INCREMENT = 1;
ALTER TABLE `Prescriptions` AUTO_INCREMENT = 1;
ALTER TABLE `Opinions` AUTO_INCREMENT = 1;

--
-- Users Table Seed (1 Admin, 9 Clients)
--
INSERT INTO `Users` (`id`, `name`, `surname`, `email`, `password`, `gender`, `role`, `telephone_number`, `date_of_birth`, `created_at`, `last_login`) VALUES
(1, 'Admin', 'Kowalski', 'admin@vetclinic.com', 'hashed_admin_password', 'Male', 'admin', 500100200, '1985-05-10 00:00:00', NOW(), NOW()),
(2, 'Anna', 'Nowak', 'anna.nowak@example.com', 'hashed_client_password', 'Female', 'client', 501111222, '1990-01-15 00:00:00', NOW(), NOW()),
(3, 'Piotr', 'Wiśniewski', 'piotr.wisniewski@example.com', 'hashed_client_password', 'Male', 'client', 502333444, '1988-03-20 00:00:00', NOW(), NOW()),
(4, 'Maria', 'Dąbrowska', 'maria.dabrowska@example.com', 'hashed_client_password', 'Female', 'client', 503555666, '1992-07-25 00:00:00', NOW(), NOW()),
(5, 'Jan', 'Lewandowski', 'jan.lewandowski@example.com', 'hashed_client_password', 'Male', 'client', 504777888, '1980-11-30 00:00:00', NOW(), NOW()),
(6, 'Zofia', 'Wójcik', 'zofia.wojcik@example.com', 'hashed_client_password', 'Female', 'client', 505999000, '1995-02-14 00:00:00', NOW(), NOW()),
(7, 'Adam', 'Kowalczyk', 'adam.kowalczyk@example.com', 'hashed_client_password', 'Male', 'client', 506123456, '1983-09-01 00:00:00', NOW(), NOW()),
(8, 'Katarzyna', 'Kamińska', 'katarzyna.kaminska@example.com', 'hashed_client_password', 'Female', 'client', 507876543, '1991-04-05 00:00:00', NOW(), NOW()),
(9, 'Tomasz', 'Zieliński', 'tomasz.zielinski@example.com', 'hashed_client_password', 'Male', 'client', 508246800, '1987-06-18 00:00:00', NOW(), NOW()),
(10, 'Julia', 'Szymańska', 'julia.szymanska@example.com', 'hashed_client_password', 'Female', 'client', 509135790, '1993-10-10 00:00:00', NOW(), NOW());


--
-- Doctors Table Seed (3 Doctors)
--
INSERT INTO `Doctors` (`id`, `name`, `surname`, `email`, `password`, `gender`, `telephone_number`, `date_of_birth`, `specialization`, `description`, `created_at`, `last_login`) VALUES
(1, 'Dr. Marek', 'Weterynarz', 'marek.weterynarz@vetclinic.com', 'hashed_doctor_password', 'Male', 600100100, '1975-01-01 00:00:00', 'Chirurgia, Onkologia', 'Specjalista chirurgii i onkologii weterynaryjnej.', NOW(), NOW()),
(2, 'Dr. Ewa', 'Lekarska', 'ewa.lekarska@vetclinic.com', 'hashed_doctor_password', 'Female', 601200200, '1980-02-02 00:00:00', 'Dermatologia, Kardiologia', 'Doświadczony dermatolog i kardiolog zwierzęcy.', NOW(), NOW()),
(3, 'Dr. Paweł', 'Zwierzakowski', 'pawel.zwierzakowski@vetclinic.com', 'hashed_doctor_password', 'Male', 602300300, '1982-03-03 00:00:00', 'Interna, Stomatologia', 'Specjalista chorób wewnętrznych i stomatologii.', NOW(), NOW());

--
-- Pets Table Seed (at least one pet per client)
--
INSERT INTO `Pets` (`id`, `user_id`, `name`, `breed`, `species`, `weight`, `gender`, `date_of_birth`, `created_at`) VALUES
(1, 2, 'Mruczek', 'Dachowiec', 'Kot', 4.5, 'Male', '2019-01-01 00:00:00', NOW()),
(2, 3, 'Azor', 'Owczarek Niemiecki', 'Pies', 35.2, 'Male', '2018-03-10 00:00:00', NOW()),
(3, 4, 'Rex', 'Labrador', 'Pies', 28.1, 'Neutered', '2017-06-05 00:00:00', NOW()),
(4, 5, 'Fafik', 'Pudel', 'Pies', 7.8, 'Male', '2020-09-20 00:00:00', NOW()),
(5, 6, 'Kropka', 'Syjamski', 'Kot', 3.9, 'Female', '2021-02-12 00:00:00', NOW()),
(6, 7, 'Burek', 'Kundel', 'Pies', 15.0, 'Male', '2016-04-22 00:00:00', NOW()),
(7, 8, 'Szaruś', 'Perski', 'Kot', 5.1, 'Spayed', '2019-07-07 00:00:00', NOW()),
(8, 9, 'Tofik', 'Golden Retriever', 'Pies', 30.5, 'Male', '2015-11-11 00:00:00', NOW()),
(9, 10, 'Zuzia', 'Beagle', 'Pies', 12.3, 'Female', '2020-01-01 00:00:00', NOW()),
(10, 2, 'Rysiek', 'Maine Coon', 'Kot', 6.0, 'Male', '2018-05-01 00:00:00', NOW());

--
-- Services Table Seed (at least 20 services, including default)
--
INSERT INTO `Services` (`id`, `name`, `description`, `price`) VALUES
(1, 'Appointment Base Cost', 'Standard cost for a veterinary appointment.', 50.00),
(2, 'Physical Examination', 'Thorough physical check-up.', 75.00),
(3, 'Vaccination (Rabies)', 'Rabies vaccine administration.', 60.00),
(4, 'Vaccination (Distemper)', 'Distemper vaccine administration.', 60.00),
(5, 'Blood Test (Basic Panel)', 'Basic blood work for general health.', 120.00),
(6, 'Urinalysis', 'Urine sample analysis.', 45.00),
(7, 'Fecal Exam', 'Fecal sample analysis for parasites.', 40.00),
(8, 'Dental Cleaning', 'Professional dental cleaning and polishing.', 250.00),
(9, 'Tooth Extraction', 'Surgical removal of a tooth.', 150.00),
(10, 'Spay/Neuter (Cat)', 'Surgical sterilization for cats.', 300.00),
(11, 'Spay/Neuter (Dog)', 'Surgical sterilization for dogs.', 450.00),
(12, 'Wound Treatment', 'Cleaning and dressing of wounds.', 80.00),
(13, 'Suture Removal', 'Removal of surgical sutures.', 30.00),
(14, 'Nail Trimming', 'Professional nail clipping.', 20.00),
(15, 'Microchipping', 'Implantation of identification microchip.', 55.00),
(16, 'X-ray (Single View)', 'Radiographic imaging.', 90.00),
(17, 'Ultrasound (Abdominal)', 'Abdominal ultrasound examination.', 180.00),
(18, 'Euthanasia Service', 'Humane euthanasia and aftercare discussion.', 200.00),
(19, 'Cremation (Individual)', 'Individual cremation service.', 250.00),
(20, 'Consultation (Nutrition)', 'Dietary and nutritional advice.', 65.00),
(21, 'Consultation (Behavior)', 'Behavioral consultation for pets.', 95.00),
(22, 'Allergy Testing', 'Diagnostic tests for allergies.', 150.00);


--
-- Drugs Table Seed (at least 20 drugs)
--
INSERT INTO `Drugs` (`id`, `name`, `dosage_form`, `strength`, `unit_of_measure`, `description`, `manufacturer`, `stock_quantity`) VALUES
(1, 'Amoxicillin', 'Tablet', '250mg', 'mg', 'Antibiotic for bacterial infections.', 'VetPharma', 1000),
(2, 'Metronidazole', 'Suspension', '50mg/ml', 'ml', 'Antibiotic and antiprotozoal.', 'AnimalHealth', 750),
(3, 'Carprofen', 'Chewable Tablet', '75mg', 'mg', 'NSAID for pain and inflammation.', 'PetMeds', 1200),
(4, 'Prednisone', 'Tablet', '5mg', 'mg', 'Corticosteroid for inflammation.', 'PharmaVet', 900),
(5, 'Flea & Tick Prevention', 'Spot-on', 'Varies', 'ml', 'Topical parasite control.', 'Bayer', 500),
(6, 'Heartworm Preventative', 'Chewable Tablet', 'Varies', 'mg', 'Monthly heartworm prevention.', 'Elanco', 600),
(7, 'Gabapentin', 'Capsule', '100mg', 'mg', 'Neuropathic pain and anxiety.', 'Generic', 800),
(8, 'Tramadol', 'Tablet', '50mg', 'mg', 'Opioid pain reliever.', 'Generic', 400),
(9, 'Cephalexin', 'Capsule', '250mg', 'mg', 'Antibiotic for skin infections.', 'VetRx', 700),
(10, 'Meloxicam', 'Oral Suspension', '1.5mg/ml', 'ml', 'NSAID for arthritis pain.', 'Boehringer Ingelheim', 650),
(11, 'Benadryl (Diphenhydramine)', 'Tablet', '25mg', 'mg', 'Antihistamine for allergies.', 'Generic', 1500),
(12, 'Famotidine', 'Tablet', '10mg', 'mg', 'H2 blocker for stomach issues.', 'Generic', 1100),
(13, 'Revolution (Selamectin)', 'Topical Solution', 'Varies', 'ml', 'Parasite control (fleas, mites, heartworm).', 'Zoetis', 300),
(14, 'Clavamox (Amoxicillin/Clavulanate)', 'Tablet', '125mg', 'mg', 'Broad-spectrum antibiotic.', 'Pfizer', 950),
(15, 'Thyroxine (Levothyroxine)', 'Tablet', '0.2mg', 'mg', 'Thyroid hormone replacement.', 'Generic', 500),
(16, 'Insulin', 'Injection', '100 units/ml', 'ml', 'For diabetes management.', 'Various', 200),
(17, 'Acepromazine', 'Tablet', '10mg', 'mg', 'Sedative/Tranquilizer.', 'Generic', 400),
(18, 'Doxycycline', 'Capsule', '100mg', 'mg', 'Tetracycline antibiotic.', 'Generic', 600),
(19, 'Panacur (Fenbendazole)', 'Granules', '222mg/g', 'mg', 'Dewormer.', 'Merck Animal Health', 700),
(20, 'Optimmune (Cyclosporine)', 'Ophthalmic Ointment', '0.2%', 'g', 'For dry eye (KCS).', 'MSD Animal Health', 250),
(21, 'Zylkene', 'Capsule', '75mg', 'mg', 'Anxiety and stress relief.', 'Vetoquinol', 300);

--
-- Appointments Table Seed
-- Appointments from previous Monday (June 9, 2025) to end of next week (Saturday June 21, 2025)
-- Each doctor has at least 2 appointments *during a single day*
-- Total 12 days * 3 doctors * 2 appointments/day = 72 appointments
--
INSERT INTO `Appointments` (`id`, `pet_id`, `doctor_id`, `status`, `reason_for_visit`, `diagnosis`, `notes`, `created_at`, `appointment_date`) VALUES
-- Day 1: Monday, June 09, 2025
(1, 1, 1, 'Completed', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-09 09:00:00', '2025-06-09 09:30:00'),
(2, 2, 1, 'Completed', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-09 10:00:00', '2025-06-09 10:30:00'),
(3, 3, 1, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-09 11:00:00', '2025-06-09 11:30:00'),
(4, 4, 2, 'Completed', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-09 09:00:00', '2025-06-09 09:30:00'),
(5, 5, 2, 'Completed', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-09 10:00:00', '2025-06-09 10:30:00'),
(6, 6, 2, 'In Progress', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-09 11:00:00', '2025-06-09 11:30:00'),
(7, 7, 3, 'Completed', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-09 09:00:00', '2025-06-09 09:30:00'),
(8, 8, 3, 'Completed', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-09 10:00:00', '2025-06-09 10:30:00'),
(9, 9, 3, 'Sheduled', 'Follow-up', 'Post-op check', 'Wound healing well.', '2025-06-09 11:00:00', '2025-06-09 11:30:00'),
-- Day 2: Tuesday, June 10, 2025
(10, 10, 1, 'Sheduled', 'Routine check-up', 'N/A', 'Checking overall health.', '2025-06-10 09:00:00', '2025-06-10 09:30:00'),
(11, 1, 1, 'Sheduled', 'Vaccination booster', 'N/A', 'Third booster shot.', '2025-06-10 10:00:00', '2025-06-10 10:30:00'),
(12, 2, 1, 'Sheduled', 'Flea check', 'N/A', 'Routine flea inspection.', '2025-06-10 11:00:00', '2025-06-10 11:30:00'),
(13, 3, 2, 'Sheduled', 'Weight monitoring', 'N/A', 'Follow-up on diet plan.', '2025-06-10 09:00:00', '2025-06-10 09:30:00'),
(14, 4, 2, 'Sheduled', 'Nail trim', 'N/A', 'Quick nail trimming.', '2025-06-10 10:00:00', '2025-06-10 10:30:00'),
(15, 5, 2, 'Sheduled', 'General health check', 'N/A', 'Overall wellness check.', '2025-06-10 11:00:00', '2025-06-10 11:30:00'),
(16, 6, 3, 'Sheduled', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-10 09:00:00', '2025-06-10 09:30:00'),
(17, 7, 3, 'Sheduled', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-10 10:00:00', '2025-06-10 10:30:00'),
(18, 8, 3, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-10 11:00:00', '2025-06-10 11:30:00'),
-- Day 3: Wednesday, June 11, 2025
(19, 9, 1, 'Sheduled', 'Vaccination', 'Routine', 'Next booster in 3 weeks.', '2025-06-11 09:00:00', '2025-06-11 09:30:00'),
(20, 10, 1, 'Sheduled', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-11 10:00:00', '2025-06-11 10:30:00'),
(21, 1, 1, 'Sheduled', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-11 11:00:00', '2025-06-11 11:30:00'),
(22, 2, 2, 'Sheduled', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-11 09:00:00', '2025-06-11 09:30:00'),
(23, 3, 2, 'Sheduled', 'Diarrhea', 'Dietary indiscretion', 'Advised bland diet.', '2025-06-11 10:00:00', '2025-06-11 10:30:00'),
(24, 4, 2, 'Sheduled', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-11 11:00:00', '2025-06-11 11:30:00'),
(25, 5, 3, 'Sheduled', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-11 09:00:00', '2025-06-11 09:30:00'),
(26, 6, 3, 'Sheduled', 'Follow-up', 'Post-op check', 'Wound healing well.', '2025-06-11 10:00:00', '2025-06-11 10:30:00'),
(27, 7, 3, 'Sheduled', 'Ear infection', 'Otitis externa', 'Prescribed ear drops.', '2025-06-11 11:00:00', '2025-06-11 11:30:00'),
-- Day 4: Thursday, June 12, 2025
(28, 8, 1, 'Sheduled', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-12 09:00:00', '2025-06-12 09:30:00'),
(29, 9, 1, 'Sheduled', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-12 10:00:00', '2025-06-12 10:30:00'),
(30, 10, 1, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-12 11:00:00', '2025-06-12 11:30:00'),
(31, 1, 2, 'Sheduled', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-12 09:00:00', '2025-06-12 09:30:00'),
(32, 2, 2, 'Sheduled', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-12 10:00:00', '2025-06-12 10:30:00'),
(33, 3, 2, 'Sheduled', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-12 11:00:00', '2025-06-12 11:30:00'),
(34, 4, 3, 'Sheduled', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-12 09:00:00', '2025-06-12 09:30:00'),
(35, 5, 3, 'Sheduled', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-12 10:00:00', '2025-06-12 10:30:00'),
(36, 6, 3, 'Sheduled', 'Follow-up', 'Post-op check', 'Wound healing well.', '2025-06-12 11:00:00', '2025-06-12 11:30:00'),
-- Day 5: Friday, June 13, 2025
(37, 7, 1, 'Sheduled', 'Ear infection', 'Otitis externa', 'Prescribed ear drops.', '2025-06-13 09:00:00', '2025-06-13 09:30:00'),
(38, 8, 1, 'Sheduled', 'Vaccination', 'Routine', 'Next booster in 3 weeks.', '2025-06-13 10:00:00', '2025-06-13 10:30:00'),
(39, 9, 1, 'Sheduled', 'Routine check-up', 'N/A', 'Checking overall health.', '2025-06-13 11:00:00', '2025-06-13 11:30:00'),
(40, 10, 2, 'Sheduled', 'Vaccination booster', 'N/A', 'Third booster shot.', '2025-06-13 09:00:00', '2025-06-13 09:30:00'),
(41, 1, 2, 'Sheduled', 'Flea check', 'N/A', 'Routine flea inspection.', '2025-06-13 10:00:00', '2025-06-13 10:30:00'),
(42, 2, 2, 'Sheduled', 'Weight monitoring', 'N/A', 'Follow-up on diet plan.', '2025-06-13 11:00:00', '2025-06-13 11:30:00'),
(43, 3, 3, 'Sheduled', 'Nail trim', 'N/A', 'Quick nail trimming.', '2025-06-13 09:00:00', '2025-06-13 09:30:00'),
(44, 4, 3, 'Sheduled', 'General health check', 'N/A', 'Overall wellness check.', '2025-06-13 10:00:00', '2025-06-13 10:30:00'),
(45, 5, 3, 'Sheduled', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-13 11:00:00', '2025-06-13 11:30:00'),
-- Day 6: Saturday, June 14, 2025
(46, 6, 1, 'Sheduled', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-14 09:00:00', '2025-06-14 09:30:00'),
(47, 7, 1, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-14 10:00:00', '2025-06-14 10:30:00'),
(48, 8, 1, 'Sheduled', 'Vaccination', 'Routine', 'Next booster in 3 weeks.', '2025-06-14 11:00:00', '2025-06-14 11:30:00'),
(49, 9, 2, 'Sheduled', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-14 09:00:00', '2025-06-14 09:30:00'),
(50, 10, 2, 'Sheduled', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-14 10:00:00', '2025-06-14 10:30:00'),
(51, 1, 2, 'Sheduled', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-14 11:00:00', '2025-06-14 11:30:00'),
(52, 2, 3, 'Sheduled', 'Diarrhea', 'Dietary indiscretion', 'Advised bland diet.', '2025-06-14 09:00:00', '2025-06-14 09:30:00'),
(53, 3, 3, 'Sheduled', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-14 10:00:00', '2025-06-14 10:30:00'),
(54, 4, 3, 'Sheduled', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-14 11:00:00', '2025-06-14 11:30:00'),
-- Day 7: Monday, June 16, 2025
(55, 5, 1, 'Sheduled', 'Follow-up', 'Post-op check', 'Wound healing well.', '2025-06-16 09:00:00', '2025-06-16 09:30:00'),
(56, 6, 1, 'Sheduled', 'Ear infection', 'Otitis externa', 'Prescribed ear drops.', '2025-06-16 10:00:00', '2025-06-16 10:30:00'),
(57, 7, 1, 'Sheduled', 'Routine check-up', 'N/A', 'Checking overall health.', '2025-06-16 11:00:00', '2025-06-16 11:30:00'),
(58, 8, 2, 'Sheduled', 'Vaccination booster', 'N/A', 'Third booster shot.', '2025-06-16 09:00:00', '2025-06-16 09:30:00'),
(59, 9, 2, 'Sheduled', 'Flea check', 'N/A', 'Routine flea inspection.', '2025-06-16 10:00:00', '2025-06-16 10:30:00'),
(60, 10, 2, 'Sheduled', 'Weight monitoring', 'N/A', 'Follow-up on diet plan.', '2025-06-16 11:00:00', '2025-06-16 11:30:00'),
(61, 1, 3, 'Sheduled', 'Nail trim', 'N/A', 'Quick nail trimming.', '2025-06-16 09:00:00', '2025-06-16 09:30:00'),
(62, 2, 3, 'Sheduled', 'General health check', 'N/A', 'Overall wellness check.', '2025-06-16 10:00:00', '2025-06-16 10:30:00'),
(63, 3, 3, 'Sheduled', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-16 11:00:00', '2025-06-16 11:30:00'),
-- Day 8: Tuesday, June 17, 2025
(64, 4, 1, 'Sheduled', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-17 09:00:00', '2025-06-17 09:30:00'),
(65, 5, 1, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-17 10:00:00', '2025-06-17 10:30:00'),
(66, 6, 1, 'Sheduled', 'Vaccination', 'Routine', 'Next booster in 3 weeks.', '2025-06-17 11:00:00', '2025-06-17 11:30:00'),
(67, 7, 2, 'Sheduled', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-17 09:00:00', '2025-06-17 09:30:00'),
(68, 8, 2, 'Sheduled', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-17 10:00:00', '2025-06-17 10:30:00'),
(69, 9, 2, 'Sheduled', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-17 11:00:00', '2025-06-17 11:30:00'),
(70, 10, 3, 'Sheduled', 'Diarrhea', 'Dietary indiscretion', 'Advised bland diet.', '2025-06-17 09:00:00', '2025-06-17 09:30:00'),
(71, 1, 3, 'Sheduled', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-17 10:00:00', '2025-06-17 10:30:00'),
(72, 2, 3, 'Sheduled', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-17 11:00:00', '2025-06-17 11:30:00'),
-- Day 9: Wednesday, June 18, 2025
(73, 3, 1, 'Sheduled', 'Follow-up', 'Post-op check', 'Wound healing well.', '2025-06-18 09:00:00', '2025-06-18 09:30:00'),
(74, 4, 1, 'Sheduled', 'Ear infection', 'Otitis externa', 'Prescribed ear drops.', '2025-06-18 10:00:00', '2025-06-18 10:30:00'),
(75, 5, 1, 'Sheduled', 'Routine check-up', 'N/A', 'Checking overall health.', '2025-06-18 11:00:00', '2025-06-18 11:30:00'),
(76, 6, 2, 'Sheduled', 'Vaccination booster', 'N/A', 'Third booster shot.', '2025-06-18 09:00:00', '2025-06-18 09:30:00'),
(77, 7, 2, 'Sheduled', 'Flea check', 'N/A', 'Routine flea inspection.', '2025-06-18 10:00:00', '2025-06-18 10:30:00'),
(78, 8, 2, 'Sheduled', 'Weight monitoring', 'N/A', 'Follow-up on diet plan.', '2025-06-18 11:00:00', '2025-06-18 11:30:00'),
(79, 9, 3, 'Sheduled', 'Nail trim', 'N/A', 'Quick nail trimming.', '2025-06-18 09:00:00', '2025-06-18 09:30:00'),
(80, 10, 3, 'Sheduled', 'General health check', 'N/A', 'Overall wellness check.', '2025-06-18 10:00:00', '2025-06-18 10:30:00'),
(81, 1, 3, 'Sheduled', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-18 11:00:00', '2025-06-18 11:30:00'),
-- Day 10: Thursday, June 19, 2025
(82, 2, 1, 'Sheduled', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-19 09:00:00', '2025-06-19 09:30:00'),
(83, 3, 1, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-19 10:00:00', '2025-06-19 10:30:00'),
(84, 4, 1, 'Sheduled', 'Vaccination', 'Routine', 'Next booster in 3 weeks.', '2025-06-19 11:00:00', '2025-06-19 11:30:00'),
(85, 5, 2, 'Sheduled', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-19 09:00:00', '2025-06-19 09:30:00'),
(86, 6, 2, 'Sheduled', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-19 10:00:00', '2025-06-19 10:30:00'),
(87, 7, 2, 'Sheduled', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-19 11:00:00', '2025-06-19 11:30:00'),
(88, 8, 3, 'Sheduled', 'Diarrhea', 'Dietary indiscretion', 'Advised bland diet.', '2025-06-19 09:00:00', '2025-06-19 09:30:00'),
(89, 9, 3, 'Sheduled', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-19 10:00:00', '2025-06-19 10:30:00'),
(90, 10, 3, 'Sheduled', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-19 11:00:00', '2025-06-19 11:30:00'),
-- Day 11: Friday, June 20, 2025
(91, 1, 1, 'Sheduled', 'Follow-up', 'Post-op check', 'Wound healing well.', '2025-06-20 09:00:00', '2025-06-20 09:30:00'),
(92, 2, 1, 'Sheduled', 'Ear infection', 'Otitis externa', 'Prescribed ear drops.', '2025-06-20 10:00:00', '2025-06-20 10:30:00'),
(93, 3, 1, 'Sheduled', 'Routine check-up', 'N/A', 'Checking overall health.', '2025-06-20 11:00:00', '2025-06-20 11:30:00'),
(94, 4, 2, 'Sheduled', 'Vaccination booster', 'N/A', 'Third booster shot.', '2025-06-20 09:00:00', '2025-06-20 09:30:00'),
(95, 5, 2, 'Sheduled', 'Flea check', 'N/A', 'Routine flea inspection.', '2025-06-20 10:00:00', '2025-06-20 10:30:00'),
(96, 6, 2, 'Sheduled', 'Weight monitoring', 'N/A', 'Follow-up on diet plan.', '2025-06-20 11:00:00', '2025-06-20 11:30:00'),
(97, 7, 3, 'Sheduled', 'Nail trim', 'N/A', 'Quick nail trimming.', '2025-06-20 09:00:00', '2025-06-20 09:30:00'),
(98, 8, 3, 'Sheduled', 'General health check', 'N/A', 'Overall wellness check.', '2025-06-20 10:00:00', '2025-06-20 10:30:00'),
(99, 9, 3, 'Sheduled', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-20 11:00:00', '2025-06-20 11:30:00'),
-- Day 12: Saturday, June 21, 2025 (End of week)
(100, 10, 1, 'Sheduled', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-21 09:00:00', '2025-06-21 09:30:00'),
(101, 1, 1, 'Sheduled', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-21 10:00:00', '2025-06-21 10:30:00'),
(102, 2, 1, 'Sheduled', 'Vaccination', 'Routine', 'Next booster in 3 weeks.', '2025-06-21 11:00:00', '2025-06-21 11:30:00'),
(103, 3, 2, 'Sheduled', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-21 09:00:00', '2025-06-21 09:30:00'),
(104, 4, 2, 'Sheduled', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-21 10:00:00', '2025-06-21 10:30:00'),
(105, 5, 2, 'Sheduled', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-21 11:00:00', '2025-06-21 11:30:00'),
(106, 6, 3, 'Sheduled', 'Diarrhea', 'Dietary indiscretion', 'Advised bland diet.', '2025-06-21 09:00:00', '2025-06-21 09:30:00'),
(107, 7, 3, 'Sheduled', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-21 10:00:00', '2025-06-21 10:30:00'),
(108, 8, 3, 'Sheduled', 'Lump on side', 'Lipoma', 'Benign, monitoring.', '2025-06-21 11:00:00', '2025-06-21 11:30:00');

--
-- Prescriptions Table Seed (most appointments have a prescription)
--
INSERT INTO `Prescriptions` (`id`, `appointment_id`, `expiry_date`, `created_at`) VALUES
(1, 1, '2025-07-09 00:00:00', NOW()), 
(2, 2, '2025-07-09 00:00:00', NOW()), 
(3, 3, '2025-07-09 00:00:00', NOW()), 
(4, 4, '2025-07-09 00:00:00', NOW()), 
(5, 5, '2025-07-09 00:00:00', NOW()), 
(6, 6, '2025-07-09 00:00:00', NOW()), 
(7, 7, '2025-07-09 00:00:00', NOW()), 
(8, 8, '2025-07-09 00:00:00', NOW()), 
(9, 9, '2025-07-09 00:00:00', NOW()), 
(10, 10, '2025-07-10 00:00:00', NOW()), 
(11, 11, '2025-07-10 00:00:00', NOW()), 
(12, 12, '2025-07-10 00:00:00', NOW()), 
(13, 13, '2025-07-10 00:00:00', NOW()), 
(14, 14, '2025-07-10 00:00:00', NOW()), 
(15, 15, '2025-07-10 00:00:00', NOW()), 
(16, 16, '2025-07-10 00:00:00', NOW()), 
(17, 17, '2025-07-10 00:00:00', NOW()), 
(18, 18, '2025-07-10 00:00:00', NOW()), 
(19, 19, '2025-07-11 00:00:00', NOW()), 
(20, 20, '2025-07-11 00:00:00', NOW()), 
(21, 21, '2025-07-11 00:00:00', NOW()), 
(22, 22, '2025-07-11 00:00:00', NOW()), 
(23, 23, '2025-07-11 00:00:00', NOW()), 
(24, 24, '2025-07-11 00:00:00', NOW()), 
(25, 25, '2025-07-11 00:00:00', NOW()), 
(26, 26, '2025-07-11 00:00:00', NOW()), 
(27, 27, '2025-07-11 00:00:00', NOW()), 
(28, 28, '2025-07-12 00:00:00', NOW()), 
(29, 29, '2025-07-12 00:00:00', NOW()), 
(30, 30, '2025-07-12 00:00:00', NOW()), 
(31, 31, '2025-07-12 00:00:00', NOW()), 
(32, 32, '2025-07-12 00:00:00', NOW()), 
(33, 33, '2025-07-12 00:00:00', NOW()), 
(34, 34, '2025-07-12 00:00:00', NOW()), 
(35, 35, '2025-07-12 00:00:00', NOW()), 
(36, 36, '2025-07-12 00:00:00', NOW()), 
(37, 37, '2025-07-13 00:00:00', NOW()), 
(38, 38, '2025-07-13 00:00:00', NOW()), 
(39, 39, '2025-07-13 00:00:00', NOW()), 
(40, 40, '2025-07-13 00:00:00', NOW()), 
(41, 41, '2025-07-13 00:00:00', NOW()), 
(42, 42, '2025-07-13 00:00:00', NOW()), 
(43, 43, '2025-07-13 00:00:00', NOW()), 
(44, 44, '2025-07-13 00:00:00', NOW()), 
(45, 45, '2025-07-13 00:00:00', NOW()), 
(46, 46, '2025-07-14 00:00:00', NOW()), 
(47, 47, '2025-07-14 00:00:00', NOW()), 
(48, 48, '2025-07-14 00:00:00', NOW()), 
(49, 49, '2025-07-14 00:00:00', NOW()), 
(50, 50, '2025-07-14 00:00:00', NOW()), 
(51, 51, '2025-07-14 00:00:00', NOW()), 
(52, 52, '2025-07-14 00:00:00', NOW()), 
(53, 53, '2025-07-14 00:00:00', NOW()), 
(54, 54, '2025-07-14 00:00:00', NOW()), 
(55, 55, '2025-07-15 00:00:00', NOW()), 
(56, 56, '2025-07-15 00:00:00', NOW()), 
(57, 57, '2025-07-15 00:00:00', NOW()), 
(58, 58, '2025-07-15 00:00:00', NOW()), 
(59, 59, '2025-07-15 00:00:00', NOW()), 
(60, 60, '2025-07-15 00:00:00', NOW()), 
(61, 61, '2025-07-15 00:00:00', NOW()), 
(62, 62, '2025-07-15 00:00:00', NOW()), 
(63, 63, '2025-07-15 00:00:00', NOW()), 
(64, 64, '2025-07-16 00:00:00', NOW()), 
(65, 65, '2025-07-16 00:00:00', NOW()), 
(66, 66, '2025-07-16 00:00:00', NOW()), 
(67, 67, '2025-07-16 00:00:00', NOW()), 
(68, 68, '2025-07-16 00:00:00', NOW()), 
(69, 69, '2025-07-16 00:00:00', NOW()), 
(70, 70, '2025-07-16 00:00:00', NOW()), 
(71, 71, '2025-07-16 00:00:00', NOW()), 
(72, 72, '2025-07-16 00:00:00', NOW()), 
(73, 73, '2025-07-17 00:00:00', NOW()), 
(74, 74, '2025-07-17 00:00:00', NOW()), 
(75, 75, '2025-07-17 00:00:00', NOW()), 
(76, 76, '2025-07-17 00:00:00', NOW()), 
(77, 77, '2025-07-17 00:00:00', NOW()), 
(78, 78, '2025-07-17 00:00:00', NOW()), 
(79, 79, '2025-07-17 00:00:00', NOW()), 
(80, 80, '2025-07-17 00:00:00', NOW()), 
(81, 81, '2025-07-17 00:00:00', NOW()), 
(82, 82, '2025-07-18 00:00:00', NOW()), 
(83, 83, '2025-07-18 00:00:00', NOW()), 
(84, 84, '2025-07-18 00:00:00', NOW()), 
(85, 85, '2025-07-18 00:00:00', NOW()), 
(86, 86, '2025-07-18 00:00:00', NOW()), 
(87, 87, '2025-07-18 00:00:00', NOW()), 
(88, 88, '2025-07-18 00:00:00', NOW()), 
(89, 89, '2025-07-18 00:00:00', NOW()), 
(90, 90, '2025-07-18 00:00:00', NOW()), 
(91, 91, '2025-07-19 00:00:00', NOW()), 
(92, 92, '2025-07-19 00:00:00', NOW()), 
(93, 93, '2025-07-19 00:00:00', NOW()), 
(94, 94, '2025-07-19 00:00:00', NOW()), 
(95, 95, '2025-07-19 00:00:00', NOW()), 
(96, 96, '2025-07-19 00:00:00', NOW()), 
(97, 97, '2025-07-19 00:00:00', NOW()), 
(98, 98, '2025-07-19 00:00:00', NOW()), 
(99, 99, '2025-07-19 00:00:00', NOW()), 
(100, 100, '2025-07-20 00:00:00', NOW()), 
(101, 101, '2025-07-20 00:00:00', NOW()), 
(102, 102, '2025-07-20 00:00:00', NOW()), 
(103, 103, '2025-07-20 00:00:00', NOW()), 
(104, 104, '2025-07-20 00:00:00', NOW()), 
(105, 105, '2025-07-20 00:00:00', NOW()), 
(106, 106, '2025-07-20 00:00:00', NOW()), 
(107, 107, '2025-07-20 00:00:00', NOW()), 
(108, 108, '2025-07-20 00:00:00', NOW());

--
-- AppointmentServices Table Seed (each appointment must have a default service)
--
INSERT INTO `AppointmentServices` (`appointment_id`, `service_id`) VALUES
(1, 1), (1, 2), (1, 3), 
(2, 1), (2, 12),       
(3, 1), (3, 8),         
(4, 1), (4, 4),         
(5, 1), (5, 2), (5, 20), 
(6, 1), (6, 5),         
(7, 1), (7, 16),        
(8, 1), (8, 7),         
(9, 1), (9, 2), (9, 6), 
(10, 1), (10, 17),      
(11, 1), (11, 13),      
(12, 1), (12, 2), (12, 22), 
(13, 1), (13, 2),       
(14, 1), (14, 3),       
(15, 1), (15, 5),       
(16, 1), (16, 20),      
(17, 1), (17, 14),      
(18, 1), (18, 2),       
(19, 1), (19, 4),       
(20, 1), (20, 5),       
(21, 1), (21, 6),       
(22, 1), (22, 7),       
(23, 1), (23, 8),       
(24, 1), (24, 9),       
(25, 1), (25, 10),      
(26, 1), (26, 11),      
(27, 1), (27, 12),      
(28, 1), (28, 13),      
(29, 1), (29, 14),      
(30, 1), (30, 15),      
(31, 1), (31, 16),      
(32, 1), (32, 17),      
(33, 1), (33, 18),      
(34, 1), (34, 19),      
(35, 1), (35, 20),      
(36, 1), (36, 21),      
(37, 1), (37, 22),      
(38, 1), (38, 2),       
(39, 1), (39, 3),       
(40, 1), (40, 4),       
(41, 1), (41, 5),       
(42, 1), (42, 6),       
(43, 1), (43, 7),       
(44, 1), (44, 8),       
(45, 1), (45, 9),       
(46, 1), (46, 10),      
(47, 1), (47, 11),      
(48, 1), (48, 12),      
(49, 1), (49, 13),      
(50, 1), (50, 14),      
(51, 1), (51, 15),      
(52, 1), (52, 16),      
(53, 1), (53, 17),      
(54, 1), (54, 18),      
(55, 1), (55, 19),      
(56, 1), (56, 20),      
(57, 1), (57, 21),      
(58, 1), (58, 22),      
(59, 1), (59, 2),       
(60, 1), (60, 3),       
(61, 1), (61, 4),       
(62, 1), (62, 5),       
(63, 1), (63, 6),       
(64, 1), (64, 7),       
(65, 1), (65, 8),       
(66, 1), (66, 9),       
(67, 1), (67, 10),      
(68, 1), (68, 11),      
(69, 1), (69, 12),      
(70, 1), (70, 13),      
(71, 1), (71, 14),      
(72, 1), (72, 15),      
(73, 1), (73, 16),      
(74, 1), (74, 17),      
(75, 1), (75, 18),      
(76, 1), (76, 19),      
(77, 1), (77, 20),      
(78, 1), (78, 21),      
(79, 1), (79, 22),      
(80, 1), (80, 2),       
(81, 1), (81, 3),       
(82, 1), (82, 4),       
(83, 1), (83, 5),       
(84, 1), (84, 6),       
(85, 1), (85, 7),       
(86, 1), (86, 8),       
(87, 1), (87, 9),       
(88, 1), (88, 10),      
(89, 1), (89, 11),      
(90, 1), (90, 12),      
(91, 1), (91, 13),      
(92, 1), (92, 14),      
(93, 1), (93, 15),      
(94, 1), (94, 16),      
(95, 1), (95, 17),      
(96, 1), (96, 18),      
(97, 1), (97, 19),      
(98, 1), (98, 20),      
(99, 1), (99, 21),      
(100, 1), (100, 22),    
(101, 1), (101, 2),     
(102, 1), (102, 3),     
(103, 1), (103, 4),     
(104, 1), (104, 5),     
(105, 1), (105, 6),     
(106, 1), (106, 7),     
(107, 1), (107, 8),     
(108, 1), (108, 9);     

--
-- PrescriptionDrugs Table Seed (at least one drug per prescription)
--
INSERT INTO `PrescriptionDrugs` (`prescription_id`, `drug_id`, `quantity`, `dosage_instructions`) VALUES
(1, 3, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(2, 11, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(3, 18, 14, 'Give 1 capsule orally once daily for 14 days.'),
(4, 2, 7, 'Give 1 ml orally twice daily for 3 days.'),
(5, 20, 1, 'Apply 1 drop to affected ear twice daily for 7 days.'),
(5, 9, 7, 'Give 1 capsule orally twice daily for 7 days.'),
(6, 1, 5, 'Give 1 tablet orally once daily.'),
(7, 5, 1, 'Apply topical solution once.'),
(8, 14, 10, 'Give 1 tablet orally twice daily.'),
(9, 17, 3, 'Give 1 tablet as needed for anxiety.'),
(10, 1, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(11, 2, 7, 'Give 1 ml orally twice daily for 3 days.'),
(12, 3, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(13, 4, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(14, 5, 1, 'Apply topical solution once.'),
(15, 6, 30, 'Give 1 chewable tablet monthly.'),
(16, 7, 20, 'Give 1 capsule orally twice daily.'),
(17, 8, 10, 'Give 1 tablet every 8 hours as needed.'),
(18, 9, 7, 'Give 1 capsule orally twice daily for 7 days.'),
(19, 10, 10, 'Give 1 ml orally once daily.'),
(20, 11, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(21, 12, 14, 'Give 1 tablet orally once daily.'),
(22, 13, 1, 'Apply topical solution once.'),
(23, 14, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(24, 15, 30, 'Give 1 tablet orally once daily.'),
(25, 16, 1, 'Inject as directed by veterinarian.'),
(26, 17, 5, 'Give 1 tablet as needed for anxiety.'),
(27, 18, 14, 'Give 1 capsule orally once daily for 14 days.'),
(28, 19, 3, 'Mix with food daily for 3 days.'),
(29, 20, 1, 'Apply 1 drop to affected eye twice daily.'),
(30, 21, 10, 'Give 1 capsule orally once daily.'),
(31, 1, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(32, 2, 7, 'Give 1 ml orally twice daily for 3 days.'),
(33, 3, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(34, 4, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(35, 5, 1, 'Apply topical solution once.'),
(36, 6, 30, 'Give 1 chewable tablet monthly.'),
(37, 7, 20, 'Give 1 capsule orally twice daily.'),
(38, 8, 10, 'Give 1 tablet every 8 hours as needed.'),
(39, 9, 7, 'Give 1 capsule orally twice daily for 7 days.'),
(40, 10, 10, 'Give 1 ml orally once daily.'),
(41, 11, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(42, 12, 14, 'Give 1 tablet orally once daily.'),
(43, 13, 1, 'Apply topical solution once.'),
(44, 14, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(45, 15, 30, 'Give 1 tablet orally once daily.'),
(46, 16, 1, 'Inject as directed by veterinarian.'),
(47, 17, 5, 'Give 1 tablet as needed for anxiety.'),
(48, 18, 14, 'Give 1 capsule orally once daily for 14 days.'),
(49, 19, 3, 'Mix with food daily for 3 days.'),
(50, 20, 1, 'Apply 1 drop to affected eye twice daily.'),
(51, 21, 10, 'Give 1 capsule orally once daily.'),
(52, 1, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(53, 2, 7, 'Give 1 ml orally twice daily for 3 days.'),
(54, 3, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(55, 4, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(56, 5, 1, 'Apply topical solution once.'),
(57, 6, 30, 'Give 1 chewable tablet monthly.'),
(58, 7, 20, 'Give 1 capsule orally twice daily.'),
(59, 8, 10, 'Give 1 tablet every 8 hours as needed.'),
(60, 9, 7, 'Give 1 capsule orally twice daily for 7 days.'),
(61, 10, 10, 'Give 1 ml orally once daily.'),
(62, 11, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(63, 12, 14, 'Give 1 tablet orally once daily.'),
(64, 13, 1, 'Apply topical solution once.'),
(65, 14, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(66, 15, 30, 'Give 1 tablet orally once daily.'),
(67, 16, 1, 'Inject as directed by veterinarian.'),
(68, 17, 5, 'Give 1 tablet as needed for anxiety.'),
(69, 18, 14, 'Give 1 capsule orally once daily for 14 days.'),
(70, 19, 3, 'Mix with food daily for 3 days.'),
(71, 20, 1, 'Apply 1 drop to affected eye twice daily.'),
(72, 21, 10, 'Give 1 capsule orally once daily.'),
(73, 1, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(74, 2, 7, 'Give 1 ml orally twice daily for 3 days.'),
(75, 3, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(76, 4, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(77, 5, 1, 'Apply topical solution once.'),
(78, 6, 30, 'Give 1 chewable tablet monthly.'),
(79, 7, 20, 'Give 1 capsule orally twice daily.'),
(80, 8, 10, 'Give 1 tablet every 8 hours as needed.'),
(81, 9, 7, 'Give 1 capsule orally twice daily for 7 days.'),
(82, 10, 10, 'Give 1 ml orally once daily.'),
(83, 11, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(84, 12, 14, 'Give 1 tablet orally once daily.'),
(85, 13, 1, 'Apply topical solution once.'),
(86, 14, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(87, 15, 30, 'Give 1 tablet orally once daily.'),
(88, 16, 1, 'Inject as directed by veterinarian.'),
(89, 17, 5, 'Give 1 tablet as needed for anxiety.'),
(90, 18, 14, 'Give 1 capsule orally once daily for 14 days.'),
(91, 19, 3, 'Mix with food daily for 3 days.'),
(92, 20, 1, 'Apply 1 drop to affected eye twice daily.'),
(93, 21, 10, 'Give 1 capsule orally once daily.'),
(94, 1, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(95, 2, 7, 'Give 1 ml orally twice daily for 3 days.'),
(96, 3, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(97, 4, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(98, 5, 1, 'Apply topical solution once.'),
(99, 6, 30, 'Give 1 chewable tablet monthly.'),
(100, 7, 20, 'Give 1 capsule orally twice daily.'),
(101, 8, 10, 'Give 1 tablet every 8 hours as needed.'),
(102, 9, 7, 'Give 1 capsule orally twice daily for 7 days.'),
(103, 10, 10, 'Give 1 ml orally once daily.'),
(104, 11, 15, 'Give 0.5 tablet orally once daily for 7 days.'),
(105, 12, 14, 'Give 1 tablet orally once daily.'),
(106, 13, 1, 'Apply topical solution once.'),
(107, 14, 10, 'Give 1 tablet orally twice daily for 5 days.'),
(108, 15, 30, 'Give 1 tablet orally once daily.');


--
-- Opinions Table Seed (each client has created an opinion to the doctor)
--
INSERT INTO `Opinions` (`id`, `user_id`, `doctor_id`, `comment`, `rating`, `created_at`) VALUES
(1, 2, 1, 'Dr. Marek is very thorough and knowledgeable. My pet Mruczek was well cared for.', 5, NOW()),
(2, 3, 1, 'Excellent service from Dr. Marek. Azor recovered quickly.', 4, NOW()),
(3, 4, 2, 'Dr. Ewa was very kind and patient with Rex. Great experience.', 5, NOW()),
(4, 5, 3, 'Dr. Paweł is amazing with nervous pets. Fafik felt comfortable.', 5, NOW()),
(5, 6, 2, 'Good advice from Dr. Ewa regarding Kropka\'s skin condition.', 4, NOW()),
(6, 7, 1, 'Adam K. here. Dr. Marek provided clear explanations for Burek\'s issues.', 4, NOW()),
(7, 8, 3, 'Katarzyna K. here. Szaruś always loves visits with Dr. Paweł.', 5, NOW()),
(8, 9, 2, 'Tomasz Z. here. Dr. Ewa was very helpful with Tofik\'s stomach problems.', 4, NOW()),
(9, 10, 3, 'Julia S. here. Zuzia got excellent care from Dr. Paweł.', 5, NOW());

-- Enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;

