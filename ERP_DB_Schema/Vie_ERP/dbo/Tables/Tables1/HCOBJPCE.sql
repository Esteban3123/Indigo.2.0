CREATE TABLE [dbo].[HCOBJPCE] (
    [CODOBJPCE] INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMOBJPCE] VARCHAR (1000) NOT NULL,
    [ESTADO]    INT            NULL,
    CONSTRAINT [PK_HCOBJPCE] PRIMARY KEY CLUSTERED ([CODOBJPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el estado   1 = Activo  2 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el nombre objetos o descripcion de planes de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'NOMOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código objetos de planes  de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';

