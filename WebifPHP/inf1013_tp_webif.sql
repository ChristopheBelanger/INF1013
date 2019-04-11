-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  jeu. 11 avr. 2019 à 18:42
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
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`id`, `email`, `password`, `full_name`, `wallet`) VALUES
(1, 'admin@system.com', '$2y$10$XkDwk94L/m3v/T/1Wmc1cevivk4DeJCLMsLVQLysp.XrqJbyh71sS', 'admin', '7840033702a152d5ae28f606a69eaeaead6773bf5c7644648cff98900df38af3'),
(2, 'admin2@system.com', '$2y$10$gcS0g8SeMCy4p6vQRnlgWOatrr0/B2hmEmZMVFAwYnU1DwX.2Kxjm', 'admin2', '9c0ed395dc2af34608210254d1f7828757bd750a86d02b63db177ec404d3dd01'),
(12, 'so@so.so', '$2y$10$aTa0zNXpgwYZT5YQ64/Wc.77KK2z1s78gEX43Ok.QP0j9hW4SI6G6', 'so', 'c9952a6be5330136ce183fef1a267d557e71f3018e21dd56b0d7f818c477a29d'),
(13, 'b@b.b', '$2y$10$nli9ouYKA7.Lb00L22hPNuT88EgfDFoV0YD3VvuxvrqqXPAmtdH2e', 'b', '06f7b0a08492076d6041114319dcac6825a7bcaa71a5f63fe664a0aca704a966'),
(14, 'f@f.f', '$2y$10$/E1SL5yb7clO6IC1pYhMNu0n8vqV3lQAFUIJaxAWLXHcQv1wk86rW', 'f', '84b692c1cfd8be8e81f7e89a0a2205686f9c5fc8678a01526c8a6d2fc599efe6'),
(15, 'r@r.r', '$2y$10$opFSlo42OMAoDKvwEbgcvecXmBtCVGl/22IfZ5XGpZoXyvm1LNa0a', 'r', 'b8261069360de03e0cb09adec3cfca8221df7bb9eae22c69c1d8140fdfea80a1'),
(16, 'q@q.q', '$2y$10$WivyR0R1lMqYkWss98QAeeItq7cjNrTQg1e52R8waTVDC1ZxH4qqu', 'q', '88419b44eb8a80436267dd8d092e2be84779e2b265203b63161c1e8e8396389f');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
