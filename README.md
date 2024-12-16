Ferramenta windows forms desenvolvida com auxilio da Microsfot Copilot seu companheiro IA.
***script para criar banco de dados sql server***
USE [master]
GO
/****** Object:  Database [erpgestao]    Script Date: 16/12/2024 01:09:32 ******/
CREATE DATABASE [erpgestao]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'erpgestao', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\erpgestao.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'erpgestao_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\erpgestao_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [erpgestao] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [erpgestao].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [erpgestao] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [erpgestao] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [erpgestao] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [erpgestao] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [erpgestao] SET ARITHABORT OFF 
GO
ALTER DATABASE [erpgestao] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [erpgestao] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [erpgestao] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [erpgestao] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [erpgestao] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [erpgestao] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [erpgestao] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [erpgestao] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [erpgestao] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [erpgestao] SET  DISABLE_BROKER 
GO
ALTER DATABASE [erpgestao] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [erpgestao] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [erpgestao] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [erpgestao] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [erpgestao] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [erpgestao] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [erpgestao] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [erpgestao] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [erpgestao] SET  MULTI_USER 
GO
ALTER DATABASE [erpgestao] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [erpgestao] SET DB_CHAINING OFF 
GO
ALTER DATABASE [erpgestao] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [erpgestao] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [erpgestao] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [erpgestao] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [erpgestao] SET QUERY_STORE = ON
GO
ALTER DATABASE [erpgestao] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [erpgestao]
GO
/****** Object:  Table [dbo].[bairro]    Script Date: 16/12/2024 01:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bairro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigobairro] [varchar](20) NULL,
	[nome] [varchar](255) NOT NULL,
	[uf] [char](2) NOT NULL,
	[cidadeid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cidade]    Script Date: 16/12/2024 01:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cidade](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](20) NULL,
	[nome] [varchar](255) NOT NULL,
	[uf] [char](2) NOT NULL,
	[estadoid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estado]    Script Date: 16/12/2024 01:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigouf] [int] NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[uf] [char](2) NOT NULL,
	[regiao] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fcfo]    Script Date: 16/12/2024 01:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fcfo](
	[fcfo_codigo] [int] IDENTITY(1,1) NOT NULL,
	[fcfo_tipo_pessoa] [char](1) NOT NULL,
	[fcfo_cpfcnpj] [varchar](18) NOT NULL,
	[fcfo_rgie] [varchar](20) NOT NULL,
	[fcfo_isento] [char](1) NOT NULL,
	[fcfo_nome_fantasia] [varchar](255) NOT NULL,
	[fcfo_razao_social] [varchar](255) NOT NULL,
	[fcfo_endereco] [varchar](255) NOT NULL,
	[fcfo_endereco_numero] [varchar](10) NOT NULL,
	[fcfo_endereco_complemento] [varchar](255) NULL,
	[fcfo_coordenada] [varchar](50) NULL,
	[fcfo_data_nascimento] [date] NULL,
	[fcfo_data_cadastro] [date] NOT NULL,
	[fcfo_nome_contato] [varchar](255) NULL,
	[fcfo_telefone1] [varchar](20) NULL,
	[fcfo_telefone2] [varchar](20) NULL,
	[fcfo_email] [varchar](255) NULL,
	[fcfo_instagram] [varchar](100) NULL,
	[fcfo_foto] [varbinary](max) NULL,
	[fcfo_qrcode] [varbinary](max) NULL,
	[fcfo_cliente] [char](1) NOT NULL,
	[fcfo_fornecedor] [char](1) NOT NULL,
	[fcfo_funcionario] [char](1) NOT NULL,
	[fcfo_membro] [char](1) NOT NULL,
	[fcfo_id_cidade] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[fcfo_codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[regiao]    Script Date: 16/12/2024 01:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[regiao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[fcfo] ADD  DEFAULT (getdate()) FOR [fcfo_data_cadastro]
GO
ALTER TABLE [dbo].[fcfo] ADD  DEFAULT ((1)) FOR [fcfo_id_cidade]
GO
ALTER TABLE [dbo].[bairro]  WITH CHECK ADD FOREIGN KEY([cidadeid])
REFERENCES [dbo].[cidade] ([id])
GO
ALTER TABLE [dbo].[bairro]  WITH CHECK ADD  CONSTRAINT [FK_bairro_cidade] FOREIGN KEY([cidadeid])
REFERENCES [dbo].[cidade] ([id])
GO
ALTER TABLE [dbo].[bairro] CHECK CONSTRAINT [FK_bairro_cidade]
GO
ALTER TABLE [dbo].[cidade]  WITH CHECK ADD FOREIGN KEY([estadoid])
REFERENCES [dbo].[estado] ([id])
GO
ALTER TABLE [dbo].[estado]  WITH CHECK ADD FOREIGN KEY([regiao])
REFERENCES [dbo].[regiao] ([Id])
GO
ALTER TABLE [dbo].[fcfo]  WITH CHECK ADD CHECK  (([fcfo_fornecedor]='N' OR [fcfo_fornecedor]='S'))
GO
ALTER TABLE [dbo].[fcfo]  WITH CHECK ADD CHECK  (([fcfo_funcionario]='N' OR [fcfo_funcionario]='S'))
GO
ALTER TABLE [dbo].[fcfo]  WITH CHECK ADD CHECK  (([fcfo_isento]='N' OR [fcfo_isento]='S'))
GO
ALTER TABLE [dbo].[fcfo]  WITH CHECK ADD CHECK  (([fcfo_membro]='N' OR [fcfo_membro]='S'))
GO
ALTER TABLE [dbo].[fcfo]  WITH CHECK ADD CHECK  (([fcfo_tipo_pessoa]='J' OR [fcfo_tipo_pessoa]='R' OR [fcfo_tipo_pessoa]='F'))
GO
USE [master]
GO
ALTER DATABASE [erpgestao] SET  READ_WRITE 
GO
