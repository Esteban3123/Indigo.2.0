CREATE TABLE [dbo].[PYPPARAHISTPERMI] (
    [ID]             INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO] INT                                                                           NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Profesional que puede ingresara la HC parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion de la tabla de Historia parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'ID';

