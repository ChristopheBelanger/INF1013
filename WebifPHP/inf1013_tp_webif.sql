-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  mer. 27 mars 2019 à 12:56
-- Version du serveur :  5.7.23
-- Version de PHP :  7.0.32

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `inf1013_tp_webif`
--

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(32) NOT NULL,
  `password` char(60) NOT NULL,
  `full_name` varchar(20) NOT NULL,
  `wallet` char(64) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `full_name` (`full_name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`id`, `email`, `password`, `full_name`, `wallet`) VALUES
(3, 'bob@bobo.bo', '$2a$08$qoYqGLthfXPp65fTv/jPmOaJ9jUV0BAZKaj9palXHk5pC96EtTizK', 'bobo', NULL),
(4, 'paul@gin.co', '$2y$10$dRVUDzhapbkilZ.UOtQM/eEMI07XdTaOvNmQjJudJEMVu3T8a6Knq', 'paul', NULL),
(5, 'admin@system.com', '$2y$10$XkDwk94L/m3v/T/1Wmc1cevivk4DeJCLMsLVQLysp.XrqJbyh71sS', 'admin', NULL);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
