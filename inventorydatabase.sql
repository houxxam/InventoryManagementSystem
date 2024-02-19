-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : lun. 19 fév. 2024 à 09:25
-- Version du serveur : 10.4.32-MariaDB
-- Version de PHP : 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `inventorydatabase`
--

-- --------------------------------------------------------

--
-- Structure de la table `categories`
--

CREATE TABLE `categories` (
  `Id` int(11) NOT NULL,
  `CategorieName` longtext NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déclencheurs `categories`
--
DELIMITER $$
CREATE TRIGGER `tr_delete_categories` AFTER DELETE ON `categories` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Delete Categorie ','<strong>',OLD.CategorieName,'</strong>'), 'delete',  CURRENT_TIMESTAMP(), OLD.UserId);
    END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tr_insert_categories` AFTER INSERT ON `categories` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Create Categorie ','<strong>',NEW.CategorieName,'</strong>'), 'insert',  CURRENT_TIMESTAMP(), NEW.UserId);
    END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tr_update_categories` AFTER UPDATE ON `categories` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Edit Categorie ','<strong>',NEW.CategorieName,'</strong>'), 'edit',  CURRENT_TIMESTAMP(), NEW.UserId);
    END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `loglists`
--

CREATE TABLE `loglists` (
  `Id` int(11) NOT NULL,
  `LogMessage` longtext NOT NULL,
  `LogType` longtext NOT NULL,
  `LogDate` datetime(6) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Structure de la table `materiels`
--

CREATE TABLE `materiels` (
  `Id` int(11) NOT NULL,
  `MaterielName` longtext NOT NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `SerialNumber` longtext NOT NULL,
  `InventoryNumber` longtext DEFAULT NULL,
  `MaterielOwner` longtext DEFAULT NULL,
  `CategorieId` int(11) NOT NULL,
  `ServiceId` int(11) NOT NULL,
  `ServiceGroupId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déclencheurs `materiels`
--
DELIMITER $$
CREATE TRIGGER `tr_delete_materiels` AFTER DELETE ON `materiels` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Delete Materiel ','<strong>',OLD.MaterielName,'</strong>'), 'delete',  CURRENT_TIMESTAMP(), OLD.UserId);
    END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tr_insert_materiels` AFTER INSERT ON `materiels` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Create Materiel ','<strong>',NEW.MaterielName,'</strong>'), 'insert',  CURRENT_TIMESTAMP(), NEW.UserId);
    END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tr_update_materiels` AFTER UPDATE ON `materiels` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Edit Materiel ','<strong>',NEW.MaterielName,'</strong>'), 'edit',  CURRENT_TIMESTAMP(), NEW.UserId);
    END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `servicegroups`
--

CREATE TABLE `servicegroups` (
  `Id` int(11) NOT NULL,
  `GroupName` longtext NOT NULL,
  `ServiceId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Structure de la table `services`
--

CREATE TABLE `services` (
  `Id` int(11) NOT NULL,
  `ServiceName` longtext NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déclencheurs `services`
--
DELIMITER $$
CREATE TRIGGER `tr_delete_services` AFTER DELETE ON `services` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Delete Service ','<strong>',OLD.ServiceName,'</strong>'), 'delete',  CURRENT_TIMESTAMP(), OLD.UserId);
    END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tr_insert_services` AFTER INSERT ON `services` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Create Service ','<strong>',NEW.ServiceName,'</strong>'), 'insert',  CURRENT_TIMESTAMP(), NEW.UserId);
    END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tr_update_services` AFTER UPDATE ON `services` FOR EACH ROW BEGIN
        INSERT INTO LogLists (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Edit Service ','<strong>',NEW.ServiceName,'</strong>'), 'edit',  CURRENT_TIMESTAMP(), NEW.UserId);
    END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `UserName` longtext NOT NULL,
  `Email` longtext DEFAULT NULL,
  `Password` longtext NOT NULL,
  `KeepLoggedIn` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Structure de la table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20240216225607_init', '7.0.2'),
('20240219082324_db_update', '7.0.2');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Categories_UserId` (`UserId`);

--
-- Index pour la table `loglists`
--
ALTER TABLE `loglists`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_LogLists_UserId` (`UserId`);

--
-- Index pour la table `materiels`
--
ALTER TABLE `materiels`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Materiels_CategorieId` (`CategorieId`),
  ADD KEY `IX_Materiels_ServiceGroupId` (`ServiceGroupId`),
  ADD KEY `IX_Materiels_ServiceId` (`ServiceId`),
  ADD KEY `IX_Materiels_UserId` (`UserId`);

--
-- Index pour la table `servicegroups`
--
ALTER TABLE `servicegroups`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_serviceGroups_ServiceId` (`ServiceId`);

--
-- Index pour la table `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Services_UserId` (`UserId`);

--
-- Index pour la table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Index pour la table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `categories`
--
ALTER TABLE `categories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `loglists`
--
ALTER TABLE `loglists`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `materiels`
--
ALTER TABLE `materiels`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `servicegroups`
--
ALTER TABLE `servicegroups`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `services`
--
ALTER TABLE `services`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `categories`
--
ALTER TABLE `categories`
  ADD CONSTRAINT `FK_Categories_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `loglists`
--
ALTER TABLE `loglists`
  ADD CONSTRAINT `FK_LogLists_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `materiels`
--
ALTER TABLE `materiels`
  ADD CONSTRAINT `FK_Materiels_Categories_CategorieId` FOREIGN KEY (`CategorieId`) REFERENCES `categories` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Materiels_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Materiels_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Materiels_serviceGroups_ServiceGroupId` FOREIGN KEY (`ServiceGroupId`) REFERENCES `servicegroups` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `servicegroups`
--
ALTER TABLE `servicegroups`
  ADD CONSTRAINT `FK_serviceGroups_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `services`
--
ALTER TABLE `services`
  ADD CONSTRAINT `FK_Services_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
