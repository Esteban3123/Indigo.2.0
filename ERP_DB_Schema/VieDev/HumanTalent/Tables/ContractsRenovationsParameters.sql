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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la cantidad(tiempo) del termino de la renovación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTermAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar la condicion de tiempo del termino de la renovación = 1-dias,  2-mes, 3-años, 4-Termino Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para el factor de renovaciones: 1-Menor o igual, 2-Igual', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la cantidad de periodos de renovación de contratos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar si la cantidad de renovaciones es indefinida o no', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'RenovationsUndefinedTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la cantidad (tiempo) del termino del contrato inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTermAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar la condicion de tiempo del termino del contrato inicial = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para el factor del contrato inicial:  1- Menor  2- Igual  3- Mayor', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para el Id del padre de otros registros en caso que alguno dependa de otro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Head';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clases de contratos 1 - Otros 2 - Aprendizaje 3 - Laboral Fijo 4 - Laboral Indefinido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ContractsRenovationsParameters', @level2type = N'COLUMN', @level2name = N'Id';

