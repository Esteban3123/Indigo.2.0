CREATE TABLE [dbo].[HCINGRESORECNAC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [NUMINGRESHIJO] CHAR (10)                                                                        NOT NULL,
    [FECHREGISTRO]  DATETIME                                                                         CONSTRAINT [DF_HCINGRESORECNAC_FECHREGIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [IPCODPACIHIJO] VARCHAR (25)                                                                     NULL,
    [TIPREGISTRO]   INT                                                                              NULL,
    CONSTRAINT [PK_HCINGRESORECNAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCINGRESORECNAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINGRESORECNAC_ADINGRESO1] FOREIGN KEY ([NUMINGRESHIJO]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINGRESORECNAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCINGRESORECNAC] NOCHECK CONSTRAINT [FK_HCINGRESORECNAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINGRESORECNAC] NOCHECK CONSTRAINT [FK_HCINGRESORECNAC_ADINGRESO1];


GO
ALTER TABLE [dbo].[HCINGRESORECNAC] NOCHECK CONSTRAINT [FK_HCINGRESORECNAC_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IDX_HCINGRESORECNAC_TIPREGISTRO]
    ON [dbo].[HCINGRESORECNAC]([TIPREGISTRO] ASC, [NUMINGRES] ASC)
    INCLUDE([NUMINGRESHIJO]);


GO
CREATE NONCLUSTERED INDEX [HCINGRESORECNAC_NUMINGRESHIJO]
    ON [dbo].[HCINGRESORECNAC]([NUMINGRESHIJO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para clasificar la relacion del ingreso madre - hijo asi:    1 - Ingreso Estancia Conjunta Con la Madre  2 - Ingreso con Hospitalizacion del bebe con medicamentos o laboratorios parametrizados. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'TIPREGISTRO';

