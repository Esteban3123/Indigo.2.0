CREATE TABLE [dbo].[HVPAQUETE] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPACSINCRO] INT                                                                              NOT NULL,
    [NUMEFOLIO]   NCHAR (10)                                                                       NULL,
    [CODAUTONU]   INT                                                                              NULL,
    [AUTOLABOR]   INT                                                                              NULL,
    [NUMMUESTRA]  INT                                                                              NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HVPAQUETE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HVPAQUETE_HVSINCRO] FOREIGN KEY ([IDPACSINCRO]) REFERENCES [dbo].[HVSINCRO] ([ID]),
    CONSTRAINT [FK_HVPAQUETE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HVPAQUETE] NOCHECK CONSTRAINT [FK_HVPAQUETE_HVSINCRO];


GO
ALTER TABLE [dbo].[HVPAQUETE] NOCHECK CONSTRAINT [FK_HVPAQUETE_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En caso de que sea cita médica, se guarda el código del autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En caso de que sea Historia, se guarda el numero del folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla HVSINCRO, que contiene los pacientes sincronizados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'IDPACSINCRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVPAQUETE', @level2type = N'COLUMN', @level2name = N'ID';

