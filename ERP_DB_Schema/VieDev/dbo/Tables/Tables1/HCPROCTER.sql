CREATE TABLE [dbo].[HCPROCTER] (
    [CODCONSEC]                NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [FECREGSIS]                DATETIME                                                                         CONSTRAINT [DF_HCPROCTER_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCPROCTER] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCPROCTER_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPROCTER_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPROCTER_CUPSEntityContractDescriptions] FOREIGN KEY ([IDDESCRIPCIONRELACIONADA]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_HCPROCTER_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPROCTER_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCPROCTER_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPROCTER_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPROCTER_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_CUPSEntityContractDescriptions];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_INESPECIA];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_INPACIENT];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPROCTER] NOCHECK CONSTRAINT [FK_HCPROCTER_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCPROCTER__NUMINGRES__INC__CODPROSAL__CODSERIPS__IPCODPACI]
    ON [dbo].[HCPROCTER]([NUMINGRES] ASC)
    INCLUDE([IPCODPACI], [CODPROSAL], [CODSERIPS]);


GO
CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL_HCPROCTER] 
   ON  [dbo].[HCPROCTER] 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from HCPROCTER hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.CODCONSEC in (select CODCONSEC from inserted)
end

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPROCTER', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Descripción Relacionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPROCTER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';

