CREATE TABLE [dbo].[AGCAUCANA] (
    [CODCAUCAN] CHAR (3)       NOT NULL,
    [DESCAUCAN] NVARCHAR (150) NOT NULL,
    [ESTCAUCAN] BIT            NOT NULL,
    CONSTRAINT [PK_AGCAUCANA] PRIMARY KEY CLUSTERED ([CODCAUCAN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado(Activo o inactivo) de la Causa de cancelación de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Causa de cancelacion de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Causa de Cancelacion de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';

