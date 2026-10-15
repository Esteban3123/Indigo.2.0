CREATE TABLE [MixingStation].[Turn] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [State]               BIT           NOT NULL,
    [WeekFraction]        INT           NOT NULL,
    [Description]         VARCHAR (MAX) NULL,
    [InitialTimeWork]     DATETIME      NULL,
    [EndingTimeWork]      DATETIME      NULL,
    [InitialTimeDelivery] DATETIME      NULL,
    [EndingTimeDelivery]  DATETIME      NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_Turn_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_Turn_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Turn] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de entrega final', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'EndingTimeDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de entrega inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'InitialTimeDelivery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de finalización del trabajo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'EndingTimeWork';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de trabajo inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'InitialTimeWork';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fracción de semana', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'WeekFraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Turn', @level2type = N'COLUMN', @level2name = N'Id';

