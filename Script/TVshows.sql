USE [TVshows]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[idCat] [int] IDENTITY(1,1) NOT NULL,
	[NameCat] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[idCat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Channels]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channels](
	[idCh] [int] IDENTITY(1,1) NOT NULL,
	[NameCh] [nvarchar](50) NOT NULL,
	[DescriptionCh] [nvarchar](100) NULL,
 CONSTRAINT [PK_Channels] PRIMARY KEY CLUSTERED 
(
	[idCh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaysOfWeek]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaysOfWeek](
	[idDayOfWeek] [int] IDENTITY(1,1) NOT NULL,
	[NameDayOfWeek] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_DaysOfWeek] PRIMARY KEY CLUSTERED 
(
	[idDayOfWeek] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[idFav] [int] IDENTITY(1,1) NOT NULL,
	[idUs] [int] NOT NULL,
	[idSh] [int] NOT NULL,
	[dtNotifyRefresh] [datetime] NOT NULL,
 CONSTRAINT [PK_Favorites] PRIMARY KEY CLUSTERED 
(
	[idFav] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[NameRol] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shows]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shows](
	[idSh] [int] IDENTITY(1,1) NOT NULL,
	[NameSh] [nvarchar](50) NOT NULL,
	[tDurationSh] [time](7) NOT NULL,
	[DescrioptionSh] [nvarchar](100) NULL,
	[idCat] [int] NULL,
 CONSTRAINT [PK_Shows] PRIMARY KEY CLUSTERED 
(
	[idSh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stencil]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stencil](
	[idSt] [int] IDENTITY(1,1) NOT NULL,
	[idCh] [int] NOT NULL,
	[idCat] [int] NULL,
	[idDayOfWeek] [int] NOT NULL,
	[tStart] [time](7) NOT NULL,
 CONSTRAINT [PK_Stencil] PRIMARY KEY CLUSTERED 
(
	[idSt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Television]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Television](
	[idTv] [int] IDENTITY(1,1) NOT NULL,
	[idSh] [int] NOT NULL,
	[idSt] [int] NOT NULL,
	[dStart] [date] NOT NULL,
 CONSTRAINT [PK_Television] PRIMARY KEY CLUSTERED 
(
	[idTv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18.11.2023 23:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[idUs] [int] IDENTITY(1,1) NOT NULL,
	[NameUs] [nvarchar](50) NOT NULL,
	[idRol] [int] NOT NULL,
	[PasswordUs] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[idUs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (1, N'Фильмы')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (2, N'Мультфильмы')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (3, N'Новости')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (4, N'Развлекательное')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (5, N'Образовательное')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (6, N'Детектив')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (7, N'Сериал')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (8, N'Анимационный сериал')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (9, N'Интервью')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (11, N'Драматический фильм')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (13, N'Телесериал')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (14, N'Триллер')
INSERT [dbo].[Categories] ([idCat], [NameCat]) VALUES (15, N'Реалити-шоу')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Channels] ON 

INSERT [dbo].[Channels] ([idCh], [NameCh], [DescriptionCh]) VALUES (1, N'Первый Канал', N'Это первый')
INSERT [dbo].[Channels] ([idCh], [NameCh], [DescriptionCh]) VALUES (2, N'СТС', N'Развлекаловка')
INSERT [dbo].[Channels] ([idCh], [NameCh], [DescriptionCh]) VALUES (3, N'Наука', N'Больше не смотрю')
INSERT [dbo].[Channels] ([idCh], [NameCh], [DescriptionCh]) VALUES (5, N'Disney', N'МУЛЬТЫЫЫЫ')
SET IDENTITY_INSERT [dbo].[Channels] OFF
GO
SET IDENTITY_INSERT [dbo].[DaysOfWeek] ON 

INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (1, N'Monday')
INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (2, N'Tuesday')
INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (3, N'Wednesday')
INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (4, N'Thursday')
INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (5, N'Friday')
INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (6, N'Saturday')
INSERT [dbo].[DaysOfWeek] ([idDayOfWeek], [NameDayOfWeek]) VALUES (7, N'Sunday')
SET IDENTITY_INSERT [dbo].[DaysOfWeek] OFF
GO
SET IDENTITY_INSERT [dbo].[Favorites] ON 

INSERT [dbo].[Favorites] ([idFav], [idUs], [idSh], [dtNotifyRefresh]) VALUES (3, 1, 2, CAST(N'2023-11-18T19:57:59.607' AS DateTime))
INSERT [dbo].[Favorites] ([idFav], [idUs], [idSh], [dtNotifyRefresh]) VALUES (8, 1, 9, CAST(N'2023-11-18T20:41:50.527' AS DateTime))
SET IDENTITY_INSERT [dbo].[Favorites] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([idRol], [NameRol]) VALUES (1, N'Администратор')
INSERT [dbo].[Roles] ([idRol], [NameRol]) VALUES (2, N'Пользователь')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Shows] ON 

INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (2, N'Новости на Первом', CAST(N'00:15:00' AS Time), NULL, 3)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (3, N'Душа', CAST(N'01:54:29' AS Time), N'Мульт от Disney и Pixar', 2)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (4, N'Локи - 1 сезон 1 серия', CAST(N'00:45:00' AS Time), N'Да', 7)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (5, N'Локи - 1 сезон 2 серия', CAST(N'00:45:00' AS Time), N'', 7)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (6, N'Локи - 1 сезон 3 серия', CAST(N'00:45:00' AS Time), N'', 7)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (7, N'Локи - 1 сезон 4 серия', CAST(N'00:45:00' AS Time), N'', 7)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (8, N'Локи - 1 сезон 5 серия', CAST(N'00:45:00' AS Time), N'', 7)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (9, N'Локи - 1 сезон 6 серия', CAST(N'00:45:00' AS Time), N'', 7)
INSERT [dbo].[Shows] ([idSh], [NameSh], [tDurationSh], [DescrioptionSh], [idCat]) VALUES (10, N'Приключения Тинтина', CAST(N'02:10:14' AS Time), N'Ещё и боевик', 6)
SET IDENTITY_INSERT [dbo].[Shows] OFF
GO
SET IDENTITY_INSERT [dbo].[Stencil] ON 

INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (1, 1, 3, 3, CAST(N'08:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (2, 1, 3, 3, CAST(N'20:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (3, 2, 7, 4, CAST(N'19:30:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (4, 2, 6, 4, CAST(N'19:20:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (5, 2, 7, 4, CAST(N'19:50:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (6, 5, 7, 6, CAST(N'08:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (7, 5, 7, 6, CAST(N'18:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (8, 5, 7, 6, CAST(N'10:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (9, 5, 7, 7, CAST(N'16:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (10, 5, 7, 6, CAST(N'12:00:00' AS Time))
INSERT [dbo].[Stencil] ([idSt], [idCh], [idCat], [idDayOfWeek], [tStart]) VALUES (11, 2, 7, 6, CAST(N'20:02:00' AS Time))
SET IDENTITY_INSERT [dbo].[Stencil] OFF
GO
SET IDENTITY_INSERT [dbo].[Television] ON 

INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (3, 2, 1, CAST(N'2023-11-15' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (4, 2, 1, CAST(N'2023-11-15' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (21, 10, 4, CAST(N'2023-11-16' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (22, 4, 3, CAST(N'2023-11-16' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (23, 5, 5, CAST(N'2023-11-16' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (24, 4, 6, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (25, 7, 7, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (26, 5, 8, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (27, 9, 9, CAST(N'2023-11-19' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (28, 9, 5, CAST(N'2023-11-23' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (29, 9, 3, CAST(N'2023-11-30' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (30, 9, 10, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (31, 9, 11, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[Television] ([idTv], [idSh], [idSt], [dStart]) VALUES (32, 5, 3, CAST(N'2023-11-23' AS Date))
SET IDENTITY_INSERT [dbo].[Television] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([idUs], [NameUs], [idRol], [PasswordUs]) VALUES (1, N'admin', 1, N'admin')
INSERT [dbo].[Users] ([idUs], [NameUs], [idRol], [PasswordUs]) VALUES (2, N'user', 2, N'user')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Shows] FOREIGN KEY([idSh])
REFERENCES [dbo].[Shows] ([idSh])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Shows]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Users] FOREIGN KEY([idUs])
REFERENCES [dbo].[Users] ([idUs])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Users]
GO
ALTER TABLE [dbo].[Shows]  WITH CHECK ADD  CONSTRAINT [FK_Shows_Categories] FOREIGN KEY([idCat])
REFERENCES [dbo].[Categories] ([idCat])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Shows] CHECK CONSTRAINT [FK_Shows_Categories]
GO
ALTER TABLE [dbo].[Stencil]  WITH CHECK ADD  CONSTRAINT [FK_Stencil_Categories] FOREIGN KEY([idCat])
REFERENCES [dbo].[Categories] ([idCat])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Stencil] CHECK CONSTRAINT [FK_Stencil_Categories]
GO
ALTER TABLE [dbo].[Stencil]  WITH CHECK ADD  CONSTRAINT [FK_Stencil_Channels] FOREIGN KEY([idCh])
REFERENCES [dbo].[Channels] ([idCh])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stencil] CHECK CONSTRAINT [FK_Stencil_Channels]
GO
ALTER TABLE [dbo].[Stencil]  WITH CHECK ADD  CONSTRAINT [FK_Stencil_DaysOfWeek] FOREIGN KEY([idDayOfWeek])
REFERENCES [dbo].[DaysOfWeek] ([idDayOfWeek])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stencil] CHECK CONSTRAINT [FK_Stencil_DaysOfWeek]
GO
ALTER TABLE [dbo].[Television]  WITH CHECK ADD  CONSTRAINT [FK_Television_Shows] FOREIGN KEY([idSh])
REFERENCES [dbo].[Shows] ([idSh])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Television] CHECK CONSTRAINT [FK_Television_Shows]
GO
ALTER TABLE [dbo].[Television]  WITH CHECK ADD  CONSTRAINT [FK_Television_Stencil] FOREIGN KEY([idSt])
REFERENCES [dbo].[Stencil] ([idSt])
GO
ALTER TABLE [dbo].[Television] CHECK CONSTRAINT [FK_Television_Stencil]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([idRol])
REFERENCES [dbo].[Roles] ([idRol])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
