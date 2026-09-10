CREATE TABLE [HumanTalent].[ContractsRenovationsParameters] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractClass]            TINYINT      NOT NULL,
    [Head]                     INT          NULL,
    [ContractFactor]           TINYINT      NULL,
    [ContractTerm]             TINYINT      NULL,
    [ContractTermAmount]       INT          NULL,
    [RenovationsUndefinedTerm] TINYINT      NULL,
    [RenovationsAmount]        INT          NULL,
    [RenovationsFactor]        TINYINT      NULL,
    [RenovationsTerm]          TINYINT      NULL,
    [RenovationsTermAmount]    INT          NULL,
    [CreationUser]             VARCHAR (50) NOT NULL,
    [CreationDate]             DATETIME     NOT NULL,
    [ModificationUser]         VARCHAR (50) NULL,
    [ModificationDate]         DATETIME     NULL,
    CONSTRAINT [PK_ContractsRenovationsParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de parámetros de renovación (DATETIME, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del parámetro de renovación (VARCHAR 50, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de parámetros de renovación (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el parámetro de renovación (VARCHAR 50, auditoría, identificación del autor)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de tiempo para el período de renovación del contrato (INT, duración en unidades definidas por RenovationsTerm)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTermAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la cantidad(tiempo) del termino de la renovación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTermAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTermAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para renovaciones: 1=días, 2=meses, 3=años, 4=término inicial (TINYINT, tipo de período)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar la condicion de tiempo del termino de la renovación = 1-dias,  2-mes, 3-años, 4-Termino Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor comparativo para renovaciones: 1=menor o igual, 2=igual (TINYINT, condición de aplicación)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el factor de renovaciones: 1-Menor o igual, 2-Igual', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de períodos de renovación permitidos para el contrato (INT, número de veces que se puede renovar)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la cantidad de periodos de renovación de contratos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si las renovaciones son indefinidas (ilimitadas) o limitadas (TINYINT, 0/1 booleano)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsUndefinedTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar si la cantidad de renovaciones es indefinida o no', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsUndefinedTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsUndefinedTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de tiempo para el término inicial del contrato (INT, duración en unidades definidas por ContractTerm)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTermAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la cantidad (tiempo) del termino del contrato inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTermAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTermAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para término inicial del contrato: 1=días, 2=meses, 3=años (TINYINT, período de vigencia)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar la condicion de tiempo del termino del contrato inicial = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor comparativo para contrato inicial: 1=menor, 2=igual, 3=mayor (TINYINT, condición laboral)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el factor del contrato inicial:  1- Menor  2- Igual  3- Mayor', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del registro padre/maestro del que depende este parámetro (INT, FK relacional para jerarquía)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Head';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el Id del padre de otros registros en caso que alguno dependa de otro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Head';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Head';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase o tipo de contrato: 1=Otros, 2=Aprendizaje, 3=Laboral Fijo, 4=Laboral Indefinido (TINYINT, clasificación)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clases de contratos 1 - Otros 2 - Aprendizaje 3 - Laboral Fijo 4 - Laboral Indefinido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de parámetros de renovación (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de renovación de contratos de talento humano. Define las reglas y condiciones bajo las cuales se renuevan los contratos laborales según su clase, duración, factor y cantidad de renovaciones permitidas.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters';
