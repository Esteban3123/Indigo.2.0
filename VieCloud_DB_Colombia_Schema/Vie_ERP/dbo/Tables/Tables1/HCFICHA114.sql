CREATE TABLE [dbo].[HCFICHA114](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDFICHANOTIFICACION] [int] NOT NULL,
	[CODDIAGNO] [char](4) NULL,
	[BirthWeightGrams] [smallint] NULL,
	[BirthHeightCm] [smallint] NULL,
	[CurrentWeightKg] [decimal](4, 1) NOT NULL,
	[CurrentHeightCm] [decimal](4, 1) NOT NULL,
	[MidUpperArmCircumferenceCm] [decimal](3, 1) NULL,
	[InsufficientWeightGain] [bit] NULL,
	[GrowthCurveFlatteningDescent] [bit] NULL,
	[NoWeightRecoveryThirdWeek] [bit] NULL,
	[Anemia] [bit] NOT NULL,
	[AcuteMalnutritionHistory] [bit] NOT NULL,
	[RecentIraEdaEpisodes] [bit] NOT NULL,
	[FeedingDifficulties] [bit] NULL,
	[InadequateFeedingPractices] [bit] NULL,
	[BrachialPerimeterBelow] [bit] NULL,
	[RecurrentPersistentInfections] [bit] NULL,
	[MotherAbsence] [bit] NOT NULL,
	[CaregiverHealthProblems] [bit] NOT NULL,
	[TeenMotherNoSupport] [bit] NOT NULL,
	[SocioeconomicVulnerability] [bit] NOT NULL,
	[HealthServiceAccessDifficulty] [bit] NOT NULL,
	[TwoOrMoreChildrenFoodInsecurity] [bit] NOT NULL,
	[ResidenceInNutritionalRiskZone] [bit] NOT NULL,
	[PrematurityHistory] [bit] NULL,
	[LowBirthWeightHistory] [bit] NULL,
	[SmallForGestationalAge] [bit] NULL,
	[VERSION] [varchar](20) NULL,
	[JSON] [varchar](max) NULL,
 CONSTRAINT [PK_HCFICHA114] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[HCFICHA114]  WITH CHECK ADD  CONSTRAINT [FK_HCFICHA114_HCFICHANOTIFICACION] FOREIGN KEY([IDFICHANOTIFICACION])
REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
GO

ALTER TABLE [dbo].[HCFICHA114] CHECK CONSTRAINT [FK_HCFICHA114_HCFICHANOTIFICACION]
GO

ALTER TABLE [dbo].[HCFICHA114]  WITH CHECK ADD  CONSTRAINT [CK_HCFICHA114_JSON] CHECK  ((isjson([JSON])=(1)))
GO

ALTER TABLE [dbo].[HCFICHA114] CHECK CONSTRAINT [CK_HCFICHA114_JSON]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único autoincremental de la ficha' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID de la ficha de notificación asociada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'IDFICHANOTIFICACION'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnóstico asociado a la ficha de notificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'CODDIAGNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Peso al nacer en gramos. Máximo 4 dígitos enteros, sin decimales ni negativos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'BirthWeightGrams'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Talla al nacer en centímetros. Máximo 2 dígitos enteros, sin decimales ni negativos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'BirthHeightCm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Peso actual en kilogramos sin ajuste a fórmula. Máximo 2 enteros y 1 decimal, sin negativos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'CurrentWeightKg'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Talla actual en centímetros. Máximo 2 enteros y 1 decimal, sin negativos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'CurrentHeightCm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Circunferencia media del brazo en cm (≥6cm y ≤30cm). Aplica para pacientes de 6 a 59 meses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'MidUpperArmCircumferenceCm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta ganancia de peso insuficiente? (1. Sí, 0. No). Aplica para menores de 6 meses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'InsufficientWeightGain'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta aplanamiento o descenso en curva de crecimiento? (1. Sí, 0. No). Aplica para menores de 0 a 2 años' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'GrowthCurveFlatteningDescent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿No recuperó el peso al nacer en la tercera semana de vida? (1. Sí, 0. No). Aplica para menores de 6 meses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'NoWeightRecoveryThirdWeek'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta anemia? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'Anemia'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Tiene antecedentes de desnutrición aguda con nuevo descenso de peso? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'AcuteMalnutritionHistory'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta episodios de IRA o EDA recientes en vulnerabilidad social? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'RecentIraEdaEpisodes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta dificultades en alimentación? (1. Sí, 0. No). Aplica para menores de 6 meses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'FeedingDifficulties'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta prácticas de alimentación inadecuadas? (1. Sí, 0. No). Aplica para pacientes de 6 a 59 meses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'InadequateFeedingPractices'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el perímetro braquial es menor a 12,5 cm (1. Sí, 0. No). Aplica y es obligatorio para pacientes de 6 a 59 meses de edad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'BrachialPerimeterBelow'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta infecciones recurrentes o persistentes? (1. Sí, 0. No). Aplica para pacientes de 6 a 59 meses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'RecurrentPersistentInfections'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Hay ausencia permanente de la madre? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'MotherAbsence'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿El cuidador presenta problemas de salud física o mental? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'CaregiverHealthProblems'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Madre adolescente sin red de apoyo? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'TeenMotherNoSupport'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta vulnerabilidad socioeconómica? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'SocioeconomicVulnerability'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Presenta dificultad de acceso a los servicios de salud? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'HealthServiceAccessDifficulty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Dos o más niños menores de 5 años en hogares con inseguridad alimentaria? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'TwoOrMoreChildrenFoodInsecurity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Reside en zona de riesgo nutricional? (1. Sí, 0. No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'ResidenceInNutritionalRiskZone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Tiene antecedente de prematurez? (1. Sí, 0. No). Aplica para pacientes de 0 a 2 años' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'PrematurityHistory'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Tiene antecedente de bajo peso al nacer? (1. Sí, 0. No). Aplica para pacientes de 0 a 2 años' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'LowBirthWeightHistory'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿Es pequeño para la edad gestacional? (1. Sí, 0. No). Aplica para pacientes de 0 a 2 años' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'SmallForGestationalAge'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Versión de la ficha de notificación. Valor nulo indica primera versión' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'VERSION'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Columnas adicionales en formato JSON para extensibilidad futura' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HCFICHA114', @level2type=N'COLUMN',@level2name=N'JSON'
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena el detalle clínico-nutricional y de riesgo psicosocial de la ficha de notificación SIVIGILA número 114, correspondiente a la vigilancia de desnutrición aguda en menores de 5 años. Registra medidas antropométricas (peso y talla al nacer y actuales, perímetro braquial), señales de alarma nutricional y factores de riesgo socioeconómico y familiar vinculados al evento notificable. Cada registro se asocia obligatoriamente a una ficha en `HCFICHANOTIFICACION` y admite extensibilidad mediante un campo JSON validado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCFICHA114';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCFICHA114';
GO
