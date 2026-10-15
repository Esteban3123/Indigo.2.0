CREATE TABLE [dbo].[HCSOPORTECACPACIENTE] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCSOPORTECAC]     INT                                                                              NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ANIO]               INT                                                                              NOT NULL,
    [FECHACREACION]      DATETIME                                                                         NOT NULL,
    [FECHAFIN]           DATETIME                                                                         NULL,
    [FECHAULTIMOSOPORTE] DATETIME                                                                         NULL,
    [COMPLETO]           BIT                                                                              NOT NULL,
    CONSTRAINT [PK_HCSOPORTECACPACIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOPORTECAC_SoporteCAC] FOREIGN KEY ([IDHCSOPORTECAC]) REFERENCES [dbo].[HCSOPORTECAC] ([ID]),
    CONSTRAINT [FK_HCSOPORTECACPACIENTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCSOPORTECACPACIENTE] NOCHECK CONSTRAINT [FK_HCSOPORTECAC_SoporteCAC];


GO
ALTER TABLE [dbo].[HCSOPORTECACPACIENTE] NOCHECK CONSTRAINT [FK_HCSOPORTECACPACIENTE_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del ultimo soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAULTIMOSOPORTE';

