-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 16, 2025 at 01:30 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `vetclinic`
--

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `prescriptiondrugs`;
DROP TABLE IF EXISTS `prescriptions`;
DROP TABLE IF EXISTS `appointmentservices`;
DROP TABLE IF EXISTS `appointments`;
DROP TABLE IF EXISTS `opinions`;
DROP TABLE IF EXISTS `pets`;
DROP TABLE IF EXISTS `drugs`;
DROP TABLE IF EXISTS `services`;
DROP TABLE IF EXISTS `doctors`;
DROP TABLE IF EXISTS `users`;

-- Drop the existing users table if it exists, to create a new one from scratch
DROP TABLE IF EXISTS `users`;

-- Create the new users table with the added date_of_birth column
CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `role` enum('client','admin') NOT NULL,
  `date_of_birth` date DEFAULT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `is_active` tinyint(1) NOT NULL DEFAULT 1,
  `last_login` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Insert data into the new users table with appropriate date_of_birth values
INSERT INTO `users` (`id`, `name`, `surname`, `email`, `password`, `role`, `date_of_birth`, `phone_number`, `address`, `created_at`, `is_active`, `last_login`) VALUES
(1, 'Alice', 'Johnson', 'alice.j@example.com', '$2b$10$p88a1EOhGjuFU.8.XjoLxOyDGBS2MQZQUlgmT184LnAUKtTbbvXlq', 'client', '1990-05-15', '111-222-3333', '123 Oak Ave', '2025-01-10 10:00:00', 1, '2025-06-16 10:00:00'),
(2, 'Bob', 'Williams', 'bob.w@example.com', 'hashed_password_2', 'client', '1988-11-20', '456 Pine St', NULL, '2025-01-15 11:00:00', 1, '2025-06-16 10:05:00'),
(3, 'Charlie', 'Brown', 'charlie.b@example.com', 'hashed_password_3', 'client', '1992-03-01', '333-444-5555', '789 Maple Dr', '2025-01-20 12:00:00', 1, '2025-06-16 10:10:00'),
(4, 'Diana', 'Miller', 'diana.m@example.com', 'hashed_password_4', 'client', '1985-07-22', '444-555-6666', '101 Elm Rd', '2025-01-25 13:00:00', 1, '2025-06-16 10:15:00'),
(5, 'Eve', 'Davis', 'eve.d@example.com', 'hashed_password_5', 'client', '1995-09-10', '555-666-7777', '202 Birch Ln', '2025-01-30 14:00:00', 1, '2025-06-16 10:20:00'),
(6, 'Frank', 'Garcia', 'frank.g@example.com', 'hashed_password_6', 'client', '1980-01-30', '666-777-8888', '303 Cedar Ct', '2025-02-05 09:00:00', 1, '2025-06-16 10:25:00'),
(7, 'Grace', 'Rodriguez', 'grace.r@example.com', 'hashed_password_7', 'client', '1993-04-05', '777-888-9999', '404 Spruce Blv', '2025-02-10 10:00:00', 1, '2025-06-16 10:30:00'),
(8, 'Henry', 'Martinez', 'henry.m@example.com', 'hashed_password_8', 'client', '1987-08-12', '888-999-0000', '505 Willow Way', '2025-02-15 11:00:00', 1, '2025-06-16 10:35:00'),
(9, 'Ivy', 'Hernandez', 'ivy.h@example.com', 'hashed_password_9', 'client', '1991-06-25', '999-000-1111', '606 Poplar Pl', '2025-02-20 12:00:00', 1, '2025-06-16 10:40:00'),
(10, 'Jack', 'Lopez', 'jack.l@example.com', 'hashed_password_10', 'client', '1983-12-18', '000-111-2222', '707 Aspen Dr', '2025-02-25 13:00:00', 1, '2025-06-16 10:45:00'),
(11, 'Karen', 'Gonzalez', 'karen.g@example.com', 'hashed_password_11', 'client', '1989-02-03', '111-111-1111', '808 Fir St', '2025-03-01 14:00:00', 1, '2025-06-16 10:50:00'),
(12, 'Liam', 'Perez', 'liam.p@example.com', 'hashed_password_12', 'client', '1994-10-08', '222-222-2222', '909 Palm Ave', '2025-03-05 09:00:00', 1, '2025-06-16 10:55:00'),
(13, 'Mia', 'Sanchez', 'mia.s@example.com', 'hashed_password_13', 'client', '1986-03-17', '333-333-3333', '111 Sycamore Ln', '2025-03-10 10:00:00', 1, '2025-06-16 11:00:00'),
(14, 'Noah', 'Ramirez', 'noah.r@example.com', 'hashed_password_14', 'client', '1990-07-29', '444-444-4444', '222 Redwood Rd', '2025-03-15 11:00:00', 1, '2025-06-16 11:05:00'),
(15, 'Olivia', 'Torres', 'olivia.t@example.com', 'hashed_password_15', 'client', '1984-09-02', '555-555-5555', '333 Cypress Ct', '2025-03-20 12:00:00', 1, '2025-06-16 11:10:00'),
(16, 'Peter', 'Flores', 'peter.f@example.com', 'hashed_password_16', 'client', '1996-01-11', '666-666-6666', '444 Magnolia Blv', '2025-03-25 13:00:00', 1, '2025-06-16 11:15:00'),
(17, 'Quinn', 'Rivera', 'quinn.r@example.com', 'hashed_password_17', 'client', '1982-05-06', '777-777-7777', '555 Acacia Way', '2025-03-30 14:00:00', 1, '2025-06-16 11:20:00'),
(18, 'Rachel', 'Gomez', 'rachel.g@example.com', 'hashed_password_18', 'client', '1997-11-14', '888-888-8888', '666 Juniper Pl', '2025-04-01 09:00:00', 1, '2025-06-16 11:25:00'),
(19, 'Sam', 'Diaz', 'sam.d@example.com', 'hashed_password_19', 'client', '1981-04-20', '999-999-9999', '777 Aspen Ct', '2025-04-05 10:00:00', 1, '2025-06-16 11:30:00'),
(20, 'Tina', 'Reyes', 'tina.r@example.com', 'hashed_password_20', 'client', '1993-08-09', '000-000-0000', '888 Willow Dr', '2025-04-10 11:00:00', 1, '2025-06-16 11:35:00'),
(21, 'Admin', 'User', 'admin@example.com', '$2b$10$0GhR7tUdlzx5U/a.Zy/YO.B.dZPB.cvIILxxDf1xdcBYZH66kxoe.', 'admin', '1975-01-01', '555-123-4567', 'Admin Office', '2025-01-01 08:00:00', 1, '2025-06-16 12:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `doctors`
--

CREATE TABLE `doctors` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `specialization` varchar(255) NOT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `date_of_birth` date DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `description` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT 1,
  `last_login` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `doctors`
--

INSERT INTO `doctors` (`id`, `name`, `surname`, `specialization`, `phone_number`, `email`, `date_of_birth`, `created_at`, `description`, `password`, `is_active`, `last_login`) VALUES
(1, 'John', 'Doe', 'General Practice', '123-456-7890', 'john.doe@vetclinic.com', '1980-05-15', '2025-01-01 09:00:00', 'Experienced general practitioner with a focus on preventative care.', '$2b$10$SNpLlLmwE.8dN30loV5lzeWI1SIZ0bg642YuHW7X3Eoa7dTdbA.7O', 1, '2025-06-16 09:00:00'),
(2, 'Jane', 'Smith', 'Dermatology', '098-765-4321', 'jane.smith@vetclinic.com', '1975-11-20', '2025-01-01 09:15:00', 'Specializes in skin conditions and allergies for all pets.', 'hashed_doctor_password_2', 1, '2025-06-16 09:30:00'),
(3, 'Robert', 'Johnson', 'Surgery', '111-222-3333', 'robert.johnson@vetclinic.com', '1982-03-10', '2025-01-01 09:30:00', 'Skilled surgeon for routine and complex procedures.', 'hashed_doctor_password_3', 1, '2025-06-16 10:00:00'),
(4, 'Emily', 'Davis', 'Cardiology', '444-555-6666', 'emily.davis@vetclinic.com', '1978-07-25', '2025-01-01 09:45:00', 'Focuses on heart health and cardiovascular diseases.', 'hashed_doctor_password_4', 1, '2025-06-16 10:15:00'),
(5, 'Michael', 'Brown', 'Internal Medicine', '777-888-9999', 'michael.brown@vetclinic.com', '1985-09-01', '2025-01-01 10:00:00', 'Diagnoses and treats complex internal diseases.', 'hashed_doctor_password_5', 1, '2025-06-16 10:45:00');

-- --------------------------------------------------------

--
-- Table structure for table `services`
--

CREATE TABLE `services` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `services`
--

INSERT INTO `services` (`id`, `name`, `description`, `price`) VALUES
(1, 'Consultation', 'General health check and consultation', 50.00),
(2, 'Vaccination', 'Routine vaccination shots', 30.00),
(3, 'Nail Trim', 'Trimming of pet nails', 15.00),
(4, 'Dermatology Exam', 'Examination for skin conditions', 75.00),
(5, 'Eye Exam', 'Examination for eye conditions', 60.00),
(6, 'Dental Cleaning', 'Professional dental cleaning', 200.00),
(7, 'Spay/Neuter', 'Surgical sterilization procedure', 300.00),
(8, 'Microchipping', 'Microchip implantation for identification', 40.00),
(9, 'Blood Work', 'Comprehensive blood panel analysis', 80.00),
(10, 'Urinalysis', 'Urine sample analysis', 45.00),
(11, 'X-Ray', 'Diagnostic imaging', 90.00),
(12, 'Ultrasound', 'Advanced diagnostic imaging', 150.00),
(13, 'Flea & Tick Treatment', 'Application of topical flea and tick medication', 35.00),
(14, 'Deworming', 'Administration of deworming medication', 25.00),
(15, 'Allergy Testing', 'Diagnostic tests for allergies', 120.00),
(16, 'Nutritional Counseling', 'Guidance on pet diet and nutrition', 55.00),
(17, 'Emergency Visit', 'Urgent care consultation', 100.00),
(18, 'Euthanasia Services', 'Compassionate end-of-life care', 250.00),
(19, 'Grooming', 'Full grooming service', 60.00),
(20, 'Pain Management', 'Assessment and treatment for chronic pain', 70.00),
(21, 'Physiotherapy Session', 'Rehabilitation exercises', 85.00),
(22, 'Behavioral Consultation', 'Addressing pet behavioral issues', 95.00),
(23, 'Surgical Follow-up', 'Post-operative check-up', 40.00),
(24, 'Senior Pet Check-up', 'Comprehensive exam for older pets', 70.00),
(25, 'Vaccine Titers', 'Blood test to measure antibody levels', 110.00),
(26, 'Travel Certificate', 'Health certificate for pet travel', 65.00),
(27, 'Dietary Supplement Consultation', 'Advice on appropriate supplements', 30.00);

-- --------------------------------------------------------

-- Drop the existing drugs table if it exists, to create a new one from scratch
DROP TABLE IF EXISTS `drugs`;

-- Create the new drugs table with the updated schema
CREATE TABLE `drugs` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `strength` varchar(50) DEFAULT NULL,
  `unit_of_measure` varchar(50) DEFAULT NULL,
  `manufacturer` varchar(255) DEFAULT NULL,
  `dosage_form` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Insert data into the new drugs table, adjusting 'unit_of_measure' to be more factual and adding 'dosage_form'
INSERT INTO `drugs` (`id`, `name`, `description`, `strength`, `unit_of_measure`, `manufacturer`, `dosage_form`) VALUES
(1, 'Amoxicillin', 'Antibiotic for bacterial infections', '250mg', 'tablets', 'PharmaCo', 'tablet'),
(2, 'Meloxicam', 'NSAID for pain and inflammation', '15mg', 'tablets', 'VetMed Inc.', 'tablet'),
(3, 'Atopica', 'Immunosuppressant for allergies', '50mg', 'capsules', 'Novartis', 'capsule'),
(4, 'Frontline Plus', 'Flea and tick prevention', 'N/A', 'doses', 'Merial', 'topical'),
(5, 'Metronidazole', 'Antibiotic/Antiprotozoal', '250mg', 'tablets', 'Generic Pharma', 'tablet'),
(6, 'Prednisone', 'Corticosteroid for inflammation', '5mg', 'tablets', 'Global Drugs', 'tablet'),
(7, 'Gabapentin', 'Pain relief and anti-anxiety', '100mg', 'capsules', 'NeuroPharm', 'capsule'),
(8, 'Rimadyl', 'NSAID for pain and arthritis', '75mg', 'chewable tablets', 'Zoetis', 'chewable'),
(9, 'Thyroxin', 'Thyroid hormone replacement', '0.2mg', 'tablets', 'EndoLab', 'tablet'),
(10, 'Tramadol', 'Opioid pain reliever', '50mg', 'tablets', 'PainRelief Co.', 'tablet'),
(11, 'VetriScience Composure', 'Behavioral health supplement', 'N/A', 'chews', 'VetriScience', 'chewable'),
(12, 'Denamarin', 'Liver support supplement', 'N/A', 'tablets', 'Nutramax', 'tablet'),
(13, 'Probiotic Paste', 'Digestive health probiotic', 'N/A', 'grams', 'BioHealth', 'oral paste'),
(14, 'Otomax', 'Ear infection treatment', 'N/A', 'grams', 'Merck Animal Health', 'ointment'),
(15, 'Simplicef', 'Cephalosporin antibiotic', '200mg', 'tablets', 'Zoetis', 'tablet'),
(16, 'NexGard', 'Flea and tick chewable', 'N/A', 'chewables', 'Merial', 'chewable'),
(17, 'FortiFlora', 'Probiotic supplement for cats and dogs', 'N/A', 'sachets', 'Purina Pro Plan', 'powder'),
(18, 'Revolution', 'Parasite control topical', 'N/A', 'doses', 'Pfizer', 'topical'),
(19, 'Clavamox', 'Potentiated penicillin antibiotic', '250mg', 'tablets', 'Zoetis', 'tablet'),
(20, 'Vetmedin', 'For congestive heart failure in dogs', '5mg', 'chewable tablets', 'Boehringer Ingelheim', 'chewable');

-- --------------------------------------------------------

--
-- Table structure for table `pets`
--

CREATE TABLE `pets` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `species` varchar(255) NOT NULL,
  `breed` varchar(255) DEFAULT NULL,
  `date_of_birth` date DEFAULT NULL,
  `gender` enum('Male','Female') DEFAULT NULL,
  `weight` decimal(5,2) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pets`
--

INSERT INTO `pets` (`id`, `user_id`, `name`, `species`, `breed`, `date_of_birth`, `gender`, `weight`, `created_at`) VALUES
(1, 1, 'Buddy', 'Dog', 'Golden Retriever', '2020-01-10', 'Male', 30.50, '2025-01-10 10:00:00'),
(2, 2, 'Whiskers', 'Cat', 'Siamese', '2018-03-22', 'Female', 4.20, '2025-01-15 11:00:00'),
(3, 3, 'Rocky', 'Dog', 'German Shepherd', '2019-07-01', 'Male', 38.00, '2025-01-20 12:00:00'),
(4, 4, 'Mittens', 'Cat', 'Maine Coon', '2021-05-18', 'Female', 6.80, '2025-01-25 13:00:00'),
(5, 5, 'Max', 'Dog', 'Labrador', '2017-11-11', 'Male', 32.10, '2025-01-30 14:00:00'),
(6, 6, 'Bella', 'Dog', 'Poodle', '2022-02-05', 'Female', 15.30, '2025-02-05 09:00:00'),
(7, 7, 'Shadow', 'Cat', 'Persian', '2019-09-30', 'Male', 5.50, '2025-02-10 10:00:00'),
(8, 8, 'Lucy', 'Dog', 'Beagle', '2018-04-12', 'Female', 12.00, '2025-02-15 11:00:00'),
(9, 9, 'Gizmo', 'Cat', 'Sphynx', '2020-10-25', 'Male', 3.80, '2025-02-20 12:00:00'),
(10, 10, 'Daisy', 'Dog', 'Dachshund', '2021-06-08', 'Female', 7.50, '2025-02-25 13:00:00'),
(11, 1, 'Oliver', 'Cat', 'Domestic Shorthair', '2023-01-15', 'Male', 4.00, '2025-03-01 14:00:00'),
(12, 2, 'Luna', 'Dog', 'Mixed Breed', '2022-04-01', 'Female', 25.00, '2025-03-05 09:00:00'),
(13, 3, 'Simba', 'Cat', 'Bengal', '2021-07-20', 'Male', 5.10, '2025-03-10 10:00:00'),
(14, 4, 'Coco', 'Dog', 'Pug', '2020-03-05', 'Female', 9.20, '2025-03-15 11:00:00'),
(15, 5, 'Oscar', 'Rabbit', 'Dutch', '2024-02-01', 'Male', 2.00, '2025-03-20 12:00:00'),
(16, 6, 'Ginger', 'Cat', 'Turkish Angora', '2022-11-20', 'Female', 4.50, '2025-03-25 13:00:00'),
(17, 7, 'Apollo', 'Dog', 'Boxer', '2019-08-10', 'Male', 30.00, '2025-03-30 14:00:00'),
(18, 8, 'Cleo', 'Cat', 'Ragdoll', '2021-06-01', 'Female', 6.00, '2025-04-01 09:00:00'),
(19, 9, 'Spike', 'Dog', 'Bulldog', '2020-05-15', 'Male', 25.00, '2025-04-05 10:00:00'),
(20, 10, 'Ruby', 'Bird', 'Cockatiel', '2023-03-10', 'Female', 0.15, '2025-04-10 11:00:00'),
(21, 11, 'Zeus', 'Dog', 'Great Dane', '2018-09-20', 'Male', 60.00, '2025-04-15 12:00:00'),
(22, 11, 'Athena', 'Cat', 'Siberian', '2020-04-01', 'Female', 7.00, '2025-04-20 13:00:00'),
(23, 12, 'Milo', 'Dog', 'Border Collie', '2021-02-28', 'Male', 20.00, '2025-04-25 14:00:00'),
(24, 12, 'Willow', 'Cat', 'Russian Blue', '2022-07-10', 'Female', 4.80, '2025-04-30 09:00:00'),
(25, 13, 'Bandit', 'Dog', 'Husky', '2019-01-05', 'Male', 28.00, '2025-05-01 10:00:00'),
(26, 13, 'Smokey', 'Cat', 'Domestic Shorthair', '2020-10-10', 'Male', 5.20, '2025-05-05 11:00:00'),
(27, 14, 'Rocky', 'Turtle', 'Red-eared Slider', '2015-06-01', 'Male', 1.50, '2025-05-10 12:00:00'),
(28, 14, 'Oreo', 'Dog', 'Dalmation', '2021-09-01', 'Female', 22.00, '2025-05-15 13:00:00'),
(29, 15, 'Pip', 'Hamster', 'Syrian', '2024-05-01', 'Male', 0.10, '2025-05-20 14:00:00'),
(30, 15, 'Storm', 'Dog', 'German Shorthaired Pointer', '2020-02-14', 'Female', 25.00, '2025-05-25 09:00:00'),
(31, 16, 'Whiskers', 'Guinea Pig', 'Abyssinian', '2024-03-20', 'Female', 1.00, '2025-05-30 10:00:00'),
(32, 16, 'Duke', 'Dog', 'Rottweiler', '2018-12-01', 'Male', 50.00, '2025-06-01 11:00:00'),
(33, 17, 'Nala', 'Cat', 'Abyssinian', '2022-01-01', 'Female', 4.00, '2025-06-05 12:00:00'),
(34, 17, 'Buster', 'Dog', 'Bulldog', '2019-06-20', 'Male', 28.00, '2025-06-10 13:00:00'),
(35, 18, 'Hazel', 'Rabbit', 'Mini Lop', '2023-09-01', 'Female', 1.50, '2025-06-15 14:00:00'),
(36, 18, 'Copper', 'Dog', 'Australian Shepherd', '2020-07-07', 'Male', 20.00, '2025-06-16 09:00:00'),
(37, 19, 'Gizmo', 'Bird', 'Budgerigar', '2024-01-01', 'Male', 0.05, '2025-06-16 09:30:00'),
(38, 19, 'Leo', 'Cat', 'Siamese', '2021-05-05', 'Male', 4.50, '2025-06-16 10:00:00'),
(39, 20, 'Penny', 'Dog', 'Corgi', '2022-08-10', 'Female', 12.00, '2025-06-16 10:30:00'),
(40, 20, 'Sushi', 'Fish', 'Betta', '2023-01-01', 'Female', 0.01, '2025-06-16 11:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `opinions`
--

CREATE TABLE `opinions` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `doctor_id` int(11) NOT NULL,
  `rating` int(11) NOT NULL,
  `comment` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `opinions`
--

INSERT INTO `opinions` (`id`, `user_id`, `doctor_id`, `rating`, `comment`, `created_at`) VALUES
(1, 1, 1, 5, 'John Doe is very caring and knowledgeable. Highly recommend!', '2025-06-15 10:00:00'),
(2, 2, 1, 4, 'Good experience, doctor was thorough.', '2025-06-15 11:00:00'),
(3, 3, 2, 5, 'Jane Smith helped my pet immensely with her skin issues.', '2025-06-15 12:00:00'),
(4, 4, 3, 4, 'Satisfied with the surgical consultation.', '2025-06-15 13:00:00'),
(5, 5, 1, 5, 'Excellent service, very professional.', '2025-06-15 14:00:00'),
(6, 6, 2, 4, 'Frank found Jane Smith very helpful for his pet.', '2025-06-15 14:15:00'),
(7, 7, 3, 5, 'Grace appreciated Robert Johnson\'s expertise.', '2025-06-15 14:30:00'),
(8, 8, 4, 4, 'Henry recommends Emily Davis.', '2025-06-15 14:45:00'),
(9, 9, 5, 5, 'Ivy had a great experience with Michael Brown.', '2025-06-15 15:00:00'),
(10, 10, 1, 3, 'Jack found John Doe to be average.', '2025-06-15 15:15:00'),
(11, 11, 2, 5, 'Karen was very happy with Jane Smith.', '2025-06-15 15:30:00'),
(12, 12, 3, 4, 'Liam provided positive feedback for Robert Johnson.', '2025-06-15 15:45:00'),
(13, 13, 4, 5, 'Mia highly recommends Emily Davis.', '2025-06-15 16:00:00'),
(14, 14, 5, 4, 'Noah had a good experience with Michael Brown.', '2025-06-15 16:15:00'),
(15, 15, 1, 5, 'Olivia praised John Doe\'s care.', '2025-06-15 16:30:00'),
(16, 16, 2, 3, 'Peter had mixed feelings about Jane Smith.', '2025-06-15 16:45:00'),
(17, 17, 3, 5, 'Quinn was very satisfied with Robert Johnson.', '2025-06-15 17:00:00'),
(18, 18, 4, 4, 'Rachel found Emily Davis competent.', '2025-06-15 17:15:00'),
(19, 19, 5, 5, 'Sam had an excellent experience with Michael Brown.', '2025-06-15 17:30:00'),
(20, 20, 1, 4, 'Tina recommends John Doe.', '2025-06-15 17:45:00');

-- --------------------------------------------------------

--
-- Table structure for table `appointments`
--

CREATE TABLE `appointments` (
  `id` int(11) NOT NULL,
  `pet_id` int(11) NOT NULL,
  `doctor_id` int(11) NOT NULL,
  `status` enum('Scheduled','In Progress','Completed','Cancelled') NOT NULL DEFAULT 'Scheduled',
  `reason_for_visit` varchar(255) NOT NULL,
  `diagnosis` varchar(255) DEFAULT NULL,
  `notes` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `appointment_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `appointments`
--

INSERT INTO `appointments` (`id`, `pet_id`, `doctor_id`, `status`, `reason_for_visit`, `diagnosis`, `notes`, `created_at`, `appointment_date`) VALUES
(1, 1, 1, 'Completed', 'Annual check-up', 'Healthy', 'Vaccinated against rabies.', '2025-06-08 09:00:00', '2025-06-09 09:30:00'),
(2, 2, 1, 'Completed', 'Limping', 'Sprained paw', 'Advised rest and NSAIDs.', '2025-06-08 10:00:00', '2025-06-09 10:30:00'),
(3, 3, 2, 'Completed', 'Skin rash', 'Allergic dermatitis', 'Prescribed antihistamines.', '2025-06-08 11:00:00', '2025-06-09 11:30:00'),
(4, 4, 3, 'Completed', 'Dental check', 'Tartar buildup', 'Recommended dental cleaning.', '2025-06-08 12:00:00', '2025-06-09 13:00:00'),
(5, 5, 4, 'Completed', 'Coughing', 'Possible kennel cough', 'Awaiting lab results.', '2025-06-09 09:00:00', '2025-06-10 09:30:00'),
(6, 6, 5, 'Completed', 'Eye discharge', 'Conjunctivitis', 'Prescribed eye drops.', '2025-06-09 10:00:00', '2025-06-10 10:30:00'),
(7, 7, 1, 'Completed', 'Loss of appetite', 'Gastroenteritis', 'Prescribed anti-nausea.', '2025-06-09 11:00:00', '2025-06-10 11:30:00'),
(8, 8, 2, 'Cancelled', 'Lump on side', NULL, 'Owner cancelled last minute.', '2025-06-09 12:00:00', '2025-06-10 13:00:00'),
(9, 9, 3, 'Completed', 'Follow-up surgery', 'Post-op check', 'Wound healing well.', '2025-06-10 09:00:00', '2025-06-11 09:30:00'),
(10, 10, 4, 'Completed', 'Routine check-up', 'Healthy', 'General wellness check.', '2025-06-10 10:00:00', '2025-06-11 10:30:00'),
(11, 11, 5, 'Completed', 'Vaccination booster', 'N/A', 'Third booster shot.', '2025-06-10 11:00:00', '2025-06-11 11:30:00'),
(12, 12, 1, 'In Progress', 'Flea check', 'N/A', 'Applying topical flea treatment during appointment.', '2025-06-10 12:00:00', '2025-06-12 09:30:00'),
(13, 13, 2, 'In Progress', 'Weight monitoring', 'Overweight', 'Discussing diet plan and exercise routine.', '2025-06-11 09:00:00', '2025-06-12 10:30:00'),
(14, 14, 3, 'Completed', 'Nail trim', 'N/A', 'Quick nail trimming.', '2025-06-11 10:00:00', '2025-06-12 11:30:00'),
(15, 15, 4, 'Completed', 'Annual check-up', 'Healthy', 'Vaccinated and dewormed.', '2025-06-11 11:00:00', '2025-06-12 13:00:00'),
(16, 16, 5, 'Completed', 'Ear infection', 'Otitis externa', 'Prescribed ear drops and cleaning instructions.', '2025-06-12 09:00:00', '2025-06-13 09:30:00'),
(17, 17, 1, 'In Progress', 'Blood work', 'Anemia suspicion', 'Waiting for lab results to confirm diagnosis.', '2025-06-12 10:00:00', '2025-06-13 10:30:00'),
(18, 18, 2, 'Completed', 'Puppy check-up', 'Healthy', 'First check-up for new puppy, advised on training.', '2025-06-12 11:00:00', '2025-06-13 11:30:00'),
(19, 19, 3, 'Completed', 'Urinary tract infection symptoms', 'UTI', 'Prescribed antibiotics and dietary changes.', '2025-06-13 09:00:00', '2025-06-14 09:30:00'),
(20, 20, 4, 'Completed', 'Broken wing', 'Fracture', 'Cast applied, follow-up in 2 weeks.', '2025-06-13 10:00:00', '2025-06-14 10:30:00'),
(21, 21, 5, 'Completed', 'Emergency visit', 'Hit by car', 'Stabilized, required immediate surgery.', '2025-06-14 09:00:00', '2025-06-15 09:30:00'),
(22, 22, 1, 'Scheduled', 'Annual check-up', NULL, 'Routine physical exam.', '2025-06-15 10:00:00', '2025-06-16 10:00:00'),
(23, 23, 2, 'Scheduled', 'Persistent scratching', NULL, 'Investigating possible allergies.', '2025-06-15 11:00:00', '2025-06-16 11:00:00'),
(24, 24, 3, 'Scheduled', 'Diarrhea', NULL, 'Owner concerned about dehydration.', '2025-06-15 12:00:00', '2025-06-16 12:00:00'),
(25, 25, 4, 'Scheduled', 'Vaccination', NULL, 'First set of puppy shots.', '2025-06-15 13:00:00', '2025-06-17 09:00:00'),
(26, 26, 5, 'Scheduled', 'Dental cleaning', NULL, 'Full dental scaling and polishing.', '2025-06-15 14:00:00', '2025-06-17 10:00:00'),
(27, 27, 1, 'Scheduled', 'Shell check', NULL, 'Routine examination for turtle.', '2025-06-15 15:00:00', '2025-06-17 11:00:00'),
(28, 28, 2, 'Scheduled', 'Follow-up ear infection', NULL, 'Checking progress after treatment.', '2025-06-15 16:00:00', '2025-06-18 09:00:00'),
(29, 29, 3, 'Scheduled', 'General health check', NULL, 'Wellness exam for hamster.', '2025-06-15 17:00:00', '2025-06-18 10:00:00'),
(30, 30, 4, 'Scheduled', 'Eye irritation', NULL, 'Possible foreign object.', '2025-06-16 09:00:00', '2025-06-18 11:00:00'),
(31, 31, 5, 'Scheduled', 'Nail Trim', NULL, 'Routine grooming.', '2025-06-16 10:00:00', '2025-06-19 09:00:00'),
(32, 32, 1, 'Scheduled', 'Senior Pet Check-up', NULL, 'Comprehensive exam for older dog.', '2025-06-16 11:00:00', '2025-06-19 10:00:00'),
(33, 33, 2, 'Scheduled', 'Vaccination', NULL, 'Annual cat vaccinations.', '2025-06-16 12:00:00', '2025-06-19 11:00:00'),
(34, 34, 3, 'Scheduled', 'Limping front leg', NULL, 'Investigation needed.', '2025-06-16 13:00:00', '2025-06-20 09:00:00'),
(35, 35, 4, 'Scheduled', 'Deworming', NULL, 'Routine deworming for rabbit.', '2025-06-16 14:00:00', '2025-06-20 10:00:00'),
(36, 36, 5, 'Scheduled', 'Skin irritation', NULL, 'Possible allergy.', '2025-06-16 15:00:00', '2025-06-20 11:00:00'),
(37, 37, 1, 'Scheduled', 'Wing clipping', NULL, 'Routine wing maintenance for bird.', '2025-06-16 16:00:00', '2025-06-21 09:00:00'),
(38, 38, 2, 'Scheduled', 'Weight management', NULL, 'Discussing diet and exercise plan.', '2025-06-16 17:00:00', '2025-06-21 10:00:00'),
(39, 39, 3, 'Scheduled', 'Annual check-up', NULL, 'Vaccinations due.', '2025-06-17 09:00:00', '2025-06-23 09:00:00'),
(40, 40, 4, 'Scheduled', 'Fin rot', NULL, 'Starting treatment plan for fish.', '2025-06-17 10:00:00', '2025-06-23 10:00:00'),
(41, 1, 1, 'Scheduled', 'Follow-up rabies vaccine', NULL, 'Ensuring all vaccinations are up to date.', '2025-06-17 11:00:00', '2025-06-23 11:00:00'),
(42, 2, 2, 'Scheduled', 'Fever', NULL, 'Investigating cause of elevated temperature.', '2025-06-17 12:00:00', '2025-06-24 09:00:00'),
(43, 3, 3, 'Scheduled', 'Behavioral consultation', NULL, 'Addressing aggression issues.', '2025-06-17 13:00:00', '2025-06-24 10:00:00'),
(44, 4, 4, 'Scheduled', 'Vaccination', NULL, 'Routine kitten vaccinations.', '2025-06-17 14:00:00', '2025-06-24 11:00:00'),
(45, 5, 5, 'Scheduled', 'Dental check-up', NULL, 'Annual dental exam.', '2025-06-17 15:00:00', '2025-06-25 09:00:00'),
(46, 6, 1, 'Scheduled', 'Lethargy', NULL, 'Owner reports decreased activity.', '2025-06-17 16:00:00', '2025-06-25 10:00:00'),
(47, 7, 2, 'Scheduled', 'Eye infection', NULL, 'Redness and discharge.', '2025-06-17 17:00:00', '2025-06-25 11:00:00'),
(48, 8, 3, 'Scheduled', 'Pre-surgical consultation', NULL, 'Discussing spay procedure.', '2025-06-18 09:00:00', '2025-06-26 09:00:00'),
(49, 9, 4, 'Scheduled', 'Annual check-up', NULL, 'Routine check for senior cat.', '2025-06-18 10:00:00', '2025-06-26 10:00:00'),
(50, 10, 5, 'Scheduled', 'Skin lesion', NULL, 'Investigating growth.', '2025-06-18 11:00:00', '2025-06-26 11:00:00'),
(51, 11, 1, 'Scheduled', 'Diarrhea follow-up', NULL, 'Checking recovery after initial treatment.', '2025-06-18 12:00:00', '2025-06-27 09:00:00'),
(52, 12, 2, 'Scheduled', 'Nail Trim', NULL, 'Routine trim.', '2025-06-18 13:00:00', '2025-06-27 10:00:00'),
(53, 13, 3, 'Scheduled', 'Respiratory issues', NULL, 'Sneezing and nasal discharge.', '2025-06-18 14:00:00', '2025-06-27 11:00:00'),
(54, 14, 4, 'Scheduled', 'Vaccination', NULL, 'Annual booster.', '2025-06-18 15:00:00', '2025-06-28 09:00:00'),
(55, 15, 5, 'Scheduled', 'Loss of appetite', NULL, 'Investigating cause.', '2025-06-18 16:00:00', '2025-06-28 10:00:00'),
(56, 16, 1, 'Scheduled', 'Microchipping', NULL, 'Implanting microchip.', '2025-06-18 17:00:00', '2025-06-28 11:00:00'),
(57, 17, 2, 'Scheduled', 'Follow-up blood work', NULL, 'Checking progress after treatment for anemia.', '2025-06-19 09:00:00', '2025-06-30 09:00:00'),
(58, 18, 3, 'Scheduled', 'Grooming consultation', NULL, 'Discussing grooming needs.', '2025-06-19 10:00:00', '2025-06-30 10:00:00'),
(59, 19, 4, 'Scheduled', 'Joint pain', NULL, 'Investigating cause of limping.', '2025-06-19 11:00:00', '2025-06-30 11:00:00'),
(60, 20, 5, 'Scheduled', 'Feather health', NULL, 'Checking for feather plucking issues.', '2025-06-19 12:00:00', '2025-07-01 09:00:00'),
(61, 21, 1, 'Scheduled', 'Post-surgical check', NULL, 'Follow-up after emergency surgery.', '2025-06-19 13:00:00', '2025-07-01 10:00:00'),
(62, 22, 2, 'Scheduled', 'Weight check', NULL, 'Monitoring weight for diet plan.', '2025-06-19 14:00:00', '2025-07-01 11:00:00'),
(63, 23, 3, 'Scheduled', 'Annual check-up', NULL, 'Routine physical and vaccinations.', '2025-06-19 15:00:00', '2025-07-02 09:00:00'),
(64, 24, 4, 'Scheduled', 'Flea & Tick treatment', NULL, 'Applying preventative treatment.', '2025-06-19 16:00:00', '2025-07-02 10:00:00'),
(65, 25, 5, 'Scheduled', 'Respiratory check', NULL, 'Coughing and wheezing.', '2025-06-19 17:00:00', '2025-07-02 11:00:00'),
(66, 26, 1, 'Scheduled', 'Behavioral consultation', NULL, 'Addressing anxiety issues.', '2025-06-20 09:00:00', '2025-07-03 09:00:00'),
(67, 27, 2, 'Scheduled', 'Routine check-up', NULL, 'Wellness exam for reptile.', '2025-06-20 10:00:00', '2025-07-03 10:00:00'),
(68, 28, 3, 'Scheduled', 'Vaccination', NULL, 'Annual booster.', '2025-06-20 11:00:00', '2025-07-03 11:00:00'),
(69, 29, 4, 'Scheduled', 'Dental check', NULL, 'Checking for overgrown teeth.', '2025-06-20 12:00:00', '2025-07-04 09:00:00'),
(70, 30, 5, 'Scheduled', 'Annual check-up', NULL, 'Routine physical exam.', '2025-06-20 13:00:00', '2025-07-04 10:00:00'),
(71, 31, 1, 'Scheduled', 'Dietary consultation', NULL, 'Advice on proper guinea pig diet.', '2025-06-20 14:00:00', '2025-07-04 11:00:00'),
(72, 32, 2, 'Scheduled', 'Joint pain', NULL, 'Evaluating arthritis management.', '2025-06-20 15:00:00', '2025-07-05 09:00:00'),
(73, 33, 3, 'Scheduled', 'Lethargy', NULL, 'Investigating cause of fatigue.', '2025-06-20 16:00:00', '2025-07-05 10:00:00'),
(74, 34, 4, 'Scheduled', 'Annual check-up', NULL, 'Vaccinations due.', '2025-06-20 17:00:00', '2025-07-05 11:00:00'),
(75, 35, 5, 'Scheduled', 'Skin irritation', NULL, 'Possible mites.', '2025-06-21 09:00:00', '2025-07-06 09:00:00'),
(76, 36, 1, 'Scheduled', 'Pre-travel health certificate', NULL, 'Required for international travel.', '2025-06-21 10:00:00', '2025-07-06 10:00:00'),
(77, 37, 2, 'Scheduled', 'Beak trim', NULL, 'Routine maintenance.', '2025-06-21 11:00:00', '2025-07-06 11:00:00'),
(78, 38, 3, 'Scheduled', 'Vomiting', NULL, 'Owner concerned about pet health.', '2025-06-21 12:00:00', '2025-07-06 12:00:00'),
(79, 39, 4, 'Scheduled', 'Limph node swelling', NULL, 'Investigation needed.', '2025-06-21 13:00:00', '2025-07-06 13:00:00'),
(80, 40, 5, 'Scheduled', 'Fin damage', NULL, 'Examining fish for injuries.', '2025-06-21 14:00:00', '2025-07-06 14:00:00');


-- --------------------------------------------------------

--
-- Table structure for table `appointmentservices`
--

CREATE TABLE `appointmentservices` (
  `appointment_id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `appointmentservices`
--

INSERT INTO `appointmentservices` (`appointment_id`, `service_id`) VALUES
(1, 1), (1, 2),
(2, 1), (2, 20),
(3, 1), (3, 4),
(4, 1), (4, 6),
(5, 1), (5, 9),
(6, 1), (6, 5),
(7, 1), (7, 17),
(9, 1), (9, 23),
(10, 1),
(11, 2),
(12, 1), (12, 13),
(13, 1), (13, 16),
(14, 3),
(15, 1), (15, 2), (15, 14),
(16, 1), (16, 14),
(17, 1), (17, 9),
(18, 1), (18, 2),
(19, 1), (19, 10),
(20, 1), (20, 11),
(21, 1), (21, 17), (21, 7),
(22, 1), (22, 2),
(23, 1), (23, 4),
(24, 1), (24, 9),
(25, 1), (25, 2),
(26, 1), (26, 6),
(27, 1),
(28, 1), (28, 23),
(29, 1),
(30, 1), (30, 5),
(31, 3),
(32, 1), (32, 24),
(33, 1), (33, 2),
(34, 1), (34, 11),
(35, 1), (35, 14),
(36, 1), (36, 4),
(37, 1),
(38, 1), (38, 16),
(39, 1), (39, 2),
(40, 1),
(41, 2),
(42, 1), (42, 9),
(43, 1), (43, 22),
(44, 2),
(45, 6),
(46, 1), (46, 9),
(47, 1), (47, 5),
(48, 1),
(49, 1), (49, 24),
(50, 1), (50, 4),
(51, 1), (51, 23),
(52, 3),
(53, 1), (53, 11),
(54, 2),
(55, 1), (55, 9),
(56, 8),
(57, 1), (57, 9),
(58, 1), (58, 19),
(59, 1), (59, 11),
(60, 1),
(61, 1), (61, 23),
(62, 1), (62, 16),
(63, 1), (63, 2),
(64, 13),
(65, 1), (65, 11),
(66, 1), (66, 22),
(67, 1),
(68, 2),
(69, 1), (69, 6),
(70, 1),
(71, 1), (71, 16),
(72, 1), (72, 20),
(73, 1), (73, 9),
(74, 1), (74, 2),
(75, 1), (75, 4),
(76, 26),
(77, 1),
(78, 1), (78, 9),
(79, 1), (79, 11),
(80, 1);


-- --------------------------------------------------------

--
-- Table structure for table `prescriptions`
--

CREATE TABLE `prescriptions` (
  `id` int(11) NOT NULL,
  `appointment_id` int(11) NOT NULL,
  `expiry_date` date NOT NULL,
  `notes` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prescriptions`
--

INSERT INTO `prescriptions` (`id`, `appointment_id`, `expiry_date`, `notes`, `created_at`) VALUES
(1, 1, '2025-12-31', 'Annual vaccination booster.', '2025-06-09 09:45:00'),
(2, 2, '2025-07-09', 'Pain medication for sprained paw.', '2025-06-09 10:45:00'),
(3, 3, '2025-07-09', 'Antihistamines for skin rash.', '2025-06-09 11:45:00'),
(4, 5, '2025-07-10', 'Cough syrup. Administer as directed.', '2025-06-10 09:45:00'),
(5, 6, '2025-07-10', 'Eye drops for conjunctivitis. Apply for 7 days.', '2025-06-10 10:45:00'),
(6, 7, '2025-07-10', 'Anti-nausea medication. Offer small, frequent meals.', '2025-06-10 11:45:00'),
(7, 9, '2025-07-11', 'Pain relief for post-operative care.', '2025-06-11 09:45:00'),
(8, 11, '2025-12-31', 'Third booster shot. Schedule next year.', '2025-06-11 11:45:00'),
(9, 12, '2025-07-12', 'Flea and tick prevention. Apply monthly.', '2025-06-12 09:45:00'),
(10, 13, '2025-09-12', 'Weight management food sample. Follow up in 4 weeks.', '2025-06-12 10:45:00'),
(11, 15, '2025-12-31', 'Annual vaccination. Keep records updated.', '2025-06-12 13:15:00'),
(12, 16, '2025-07-13', 'Ear drops for otitis externa. Clean ears before application.', '2025-06-13 09:45:00'),
(13, 17, '2025-07-13', 'Iron supplement for suspected anemia. Recheck blood work in 2 weeks.', '2025-06-13 10:45:00'),
(14, 19, '2025-07-14', 'Antibiotics for UTI. Finish the course of medication.', '2025-06-14 09:45:00'),
(15, 20, '2025-07-28', 'Pain medication for broken wing. Rest is crucial.', '2025-06-14 10:45:00'),
(16, 21, '2025-07-15', 'Post-surgery pain management. Administer as directed.', '2025-06-15 09:45:00'),
(17, 23, '2025-07-16', 'Allergy medication. Monitor pet for continued scratching.', '2025-06-16 11:15:00'),
(18, 24, '2025-06-23', 'Anti-diarrhea medication. Bland diet advised.', '2025-06-16 12:15:00'),
(19, 28, '2025-07-18', 'Follow-up ear drops. Continue for 5 more days.', '2025-06-18 09:15:00'),
(20, 30, '2025-07-18', 'Eye drops for irritation. Keep eye clean.', '2025-06-18 11:15:00'),
(21, 32, '2025-09-19', 'Joint support supplement for senior pet.', '2025-06-19 10:15:00'),
(22, 34, '2025-07-20', 'Anti-inflammatory for limping. Restrict activity.', '2025-06-20 09:15:00'),
(23, 35, '2025-07-20', 'Deworming medication. Administer orally.', '2025-06-20 10:15:00'),
(24, 36, '2025-07-20', 'Topical cream for skin irritation. Apply twice daily.', '2025-06-20 11:15:00'),
(25, 40, '2025-07-23', 'Antibiotics for fin rot. Follow treatment protocol.', '2025-06-23 10:15:00'),
(26, 42, '2025-07-24', 'Antipyretic for fever. Monitor temperature.', '2025-06-24 09:15:00'),
(27, 43, '2025-08-24', 'Anxiety medication. Start at low dose.', '2025-06-24 10:15:00'),
(28, 47, '2025-07-25', 'Eye ointment for infection. Apply 3 times daily.', '2025-06-25 11:15:00'),
(29, 50, '2025-07-26', 'Anti-inflammatory for skin lesion. Biopsy if no improvement.', '2025-06-26 11:15:00'),
(30, 53, '2025-07-27', 'Antibiotics for respiratory issues. Complete course.', '2025-06-27 11:15:00'),
(31, 55, '2025-07-28', 'Appetite stimulant. Offer palatable food.', '2025-06-28 10:15:00'),
(32, 59, '2025-07-30', 'Pain relief for joint pain. Restrict vigorous activity.', '2025-06-30 11:15:00'),
(33, 65, '2025-08-02', 'Bronchodilator for wheezing. Administer as needed.', '2025-07-02 11:15:00'),
(34, 72, '2025-08-05', 'NSAID for arthritis. Long-term management.', '2025-07-05 09:15:00'),
(35, 73, '2025-08-05', 'Vitamin supplements for lethargy. Ensure balanced diet.', '2025-07-05 10:15:00'),
(36, 75, '2025-08-06', 'Anti-parasitic treatment for mites. Clean environment.', '2025-07-06 09:15:00'),
(37, 78, '2025-08-06', 'Anti-emetic for vomiting. Monitor for dehydration.', '2025-07-06 12:15:00'),
(38, 79, '2025-08-06', 'Antibiotics for lymph node swelling. Follow up in 1 week.', '2025-07-06 13:15:00');


-- --------------------------------------------------------

--
-- Table structure for table `prescriptiondrugs`
--

CREATE TABLE `prescriptiondrugs` (
  `prescription_id` int(11) NOT NULL,
  `drug_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `dosage` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prescriptiondrugs`
--

INSERT INTO `prescriptiondrugs` (`prescription_id`, `drug_id`, `quantity`, `dosage`) VALUES
(2, 2, 10, '5mg twice daily'),
(3, 3, 30, '1 tablet daily'),
(4, 6, 14, '5mg once daily'),
(5, 14, 1, '3 drops in each eye twice daily'),
(6, 5, 14, '1 tablet twice daily'),
(7, 2, 10, '15mg once daily'),
(9, 4, 1, 'Apply monthly'),
(10, 11, 30, '1 chewable daily'),
(12, 14, 1, '4 drops in affected ear twice daily'),
(13, 12, 30, '1 tablet daily'),
(14, 1, 14, '250mg twice daily'),
(15, 2, 10, '15mg once daily'),
(16, 20, 7, '5mg twice daily'),
(17, 3, 30, '1 tablet daily'),
(18, 5, 14, '1 tablet twice daily'),
(19, 14, 1, '4 drops in affected ear twice daily'),
(20, 14, 1, '3 drops in each eye twice daily'),
(21, 11, 60, '1 chewable daily'),
(22, 2, 10, '15mg once daily'),
(23, 14, 1, 'Administer orally once'),
(24, 6, 1, 'Apply thin layer to affected area twice daily'),
(25, 1, 7, '250mg once daily'),
(26, 6, 10, '5mg once daily'),
(27, 7, 30, '100mg once daily'),
(28, 14, 1, 'Apply thin layer to affected eye 3 times daily'),
(29, 6, 10, '5mg once daily'),
(30, 1, 14, '250mg twice daily'),
(31, 13, 1, '1 tube daily'),
(32, 2, 10, '15mg once daily'),
(33, 19, 14, '250mg twice daily'),
(34, 8, 30, '75mg daily'),
(35, 9, 30, '0.2mg daily'),
(36, 4, 1, 'Apply once'),
(37, 5, 7, '250mg twice daily'),
(38, 1, 14, '250mg twice daily');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `appointments`
--
ALTER TABLE `appointments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_doctor_appointments` (`doctor_id`),
  ADD KEY `fk_pet_appointments` (`pet_id`);

--
-- Indexes for table `appointmentservices`
--
ALTER TABLE `appointmentservices`
  ADD PRIMARY KEY (`appointment_id`,`service_id`),
  ADD KEY `fk_service_app_ser` (`service_id`);

--
-- Indexes for table `doctors`
--
ALTER TABLE `doctors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `drugs`
--
ALTER TABLE `drugs`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `opinions`
--
ALTER TABLE `opinions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_comments` (`user_id`),
  ADD KEY `fk_doctor_comments` (`doctor_id`);

--
-- Indexes for table `pets`
--
ALTER TABLE `pets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_pets` (`user_id`);

--
-- Indexes for table `prescriptiondrugs`
--
ALTER TABLE `prescriptiondrugs`
  ADD PRIMARY KEY (`prescription_id`,`drug_id`),
  ADD KEY `fk_drug_pre_drug` (`drug_id`);

--
-- Indexes for table `prescriptions`
--
ALTER TABLE `prescriptions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_prescription_appointment` (`appointment_id`);

--
-- Indexes for table `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `appointments`
--
ALTER TABLE `appointments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=81;

--
-- AUTO_INCREMENT for table `doctors`
--
ALTER TABLE `doctors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `drugs`
--
ALTER TABLE `drugs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `opinions`
--
ALTER TABLE `opinions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `pets`
--
ALTER TABLE `pets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT for table `prescriptions`
--
ALTER TABLE `prescriptions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT for table `services`
--
ALTER TABLE `services`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `appointments`
--
ALTER TABLE `appointments`
  ADD CONSTRAINT `fk_doctor_appointments` FOREIGN KEY (`doctor_id`) REFERENCES `doctors` (`id`),
  ADD CONSTRAINT `fk_pet_appointments` FOREIGN KEY (`pet_id`) REFERENCES `pets` (`id`);

--
-- Constraints for table `appointmentservices`
--
ALTER TABLE `appointmentservices`
  ADD CONSTRAINT `fk_appointment_app_ser` FOREIGN KEY (`appointment_id`) REFERENCES `appointments` (`id`),
  ADD CONSTRAINT `fk_service_app_ser` FOREIGN KEY (`service_id`) REFERENCES `services` (`id`);

--
-- Constraints for table `opinions`
--
ALTER TABLE `opinions`
  ADD CONSTRAINT `fk_doctor_comments` FOREIGN KEY (`doctor_id`) REFERENCES `doctors` (`id`),
  ADD CONSTRAINT `fk_user_comments` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Constraints for table `pets`
--
ALTER TABLE `pets`
  ADD CONSTRAINT `fk_user_pets` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Constraints for table `prescriptiondrugs`
--
ALTER TABLE `prescriptiondrugs`
  ADD CONSTRAINT `fk_drug_pre_drug` FOREIGN KEY (`drug_id`) REFERENCES `drugs` (`id`),
  ADD CONSTRAINT `fk_prescription_pre_drug` FOREIGN KEY (`prescription_id`) REFERENCES `prescriptions` (`id`);

--
-- Constraints for table `prescriptions`
--
ALTER TABLE `prescriptions`
  ADD CONSTRAINT `fk_prescription_appointment` FOREIGN KEY (`appointment_id`) REFERENCES `appointments` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;