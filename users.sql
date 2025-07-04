-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 04, 2025 at 02:13 PM
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
-- Database: `users`
--

-- --------------------------------------------------------

--
-- Table structure for table `access_function`
--

CREATE TABLE `access_function` (
  `AccessId` varchar(20) NOT NULL,
  `AccessName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `access_function`
--

INSERT INTO `access_function` (`AccessId`, `AccessName`) VALUES
('inv001', 'Inventory Check'),
('ord002', 'Order Creation'),
('reg003', 'Register User');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserId` varchar(20) NOT NULL,
  `Username` varchar(20) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `FirstName` varchar(20) NOT NULL,
  `LastName` varchar(20) NOT NULL,
  `CreatedDateTime` datetime(6) NOT NULL DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserId`, `Username`, `Password`, `FirstName`, `LastName`, `CreatedDateTime`) VALUES
('admin', 'admin', 'admin', 'admin', 'admin', '2025-07-04 12:04:21.000000');

-- --------------------------------------------------------

--
-- Table structure for table `users_access`
--

CREATE TABLE `users_access` (
  `UserAccessId` int(11) NOT NULL,
  `AccessId` varchar(20) NOT NULL,
  `UserId` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users_access`
--

INSERT INTO `users_access` (`UserAccessId`, `AccessId`, `UserId`) VALUES
(1, 'inv001', 'admin'),
(2, 'ord002', 'admin'),
(3, 'reg003', 'admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `access_function`
--
ALTER TABLE `access_function`
  ADD PRIMARY KEY (`AccessId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserId`);

--
-- Indexes for table `users_access`
--
ALTER TABLE `users_access`
  ADD PRIMARY KEY (`UserAccessId`),
  ADD KEY `userId` (`UserId`),
  ADD KEY `accessId` (`AccessId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `users_access`
--
ALTER TABLE `users_access`
  MODIFY `UserAccessId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `users_access`
--
ALTER TABLE `users_access`
  ADD CONSTRAINT `accessId` FOREIGN KEY (`AccessId`) REFERENCES `access_function` (`AccessId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `userId` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE CASCADE ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
