CREATE TABLE [dbo].[INDIAGNOP] (
    [IDETIPHIS]          CHAR (9)                                                                         NULL,
    [NUMEFOLIO]          NCHAR (10)                                                                       NULL,
    [CODCENATE]          CHAR (10)                                                                        NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI]          BIT                                                                              NOT NULL,
    [DIAINGEGR]          CHAR (1)                                                                         NOT NULL,
    [TIPDIAGNO]          CHAR (1)                                                                         NOT NULL,
    [CLADIAGNO]          CHAR (2)                                                                         NOT NULL,
    [OBSDIAGNO]          CHAR (250)                                                                       NOT NULL,
    [FECDIAGNO]          DATETIME                                                                         NOT NULL,
    [FOLDIAGNO]          INT                                                                              NULL,
    [DIAESTADO]          INT                                                                              NULL,
    [INDAUDFOR]          NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG]          TEXT                                                                             NULL,
    [TRATA4505]          INT                                                                              NULL,
    [FECHLEISH]          DATETIME                                                                         NULL,
    [T1]                 CHAR (2)                                                                         NULL,
    [T2]                 CHAR (2)                                                                         NULL,
    [N1]                 CHAR (2)                                                                         NULL,
    [N2]                 CHAR (2)                                                                         NULL,
    [M1]                 CHAR (2)                                                                         NULL,
    [M2]                 CHAR (2)                                                                         NULL,
    [ESTADIO]            INT                                                                              NULL,
    [IDADENFHUERFANAS]   INT                                                                              NULL,
    [TIPOCANCER]         INT                                                                              NULL,
    [ESTADIO2]           CHAR (10)                                                                        NULL,
    [CONFIRMATNMESTADIO] BIT                                                                              NULL,
    CONSTRAINT [PK_INDIAGNOP] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC, [CODDIAPRI] ASC),
    CONSTRAINT [FK_INDIAGNOP_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNOP_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_INDIAGNOP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNOP_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNOP_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNOP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNOP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_INDIAGNOS];


GO
ALTER TABLE [dbo].[INDIAGNOP] NOCHECK CONSTRAINT [FK_INDIAGNOP_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOP].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_INDIAGNOP_DOS_DIAGNOSTICOPRINCIPALES]
    ON [dbo].[INDIAGNOP]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [CODDIAPRI] ASC) WHERE ([CODDIAPRI]=(1));


GO
CREATE NONCLUSTERED INDEX [_dta_index_INDIAGNOP_6_1902629821__K7]
    ON [dbo].[INDIAGNOP]([CODDIAGNO] ASC);


GO

Create trigger [dbo].[DetectarDobleDiagnosticosEnLaHistoria]
on [dbo].[INDIAGNOP]
for insert,update as
begin
    declare @Ingreso as varchar(10) = (select  NUMINGRES  from inserted)
	declare @NUMEFOLIO as varchar(10) = (select NUMEFOLIO   from inserted)
			
	   declare @ExisteDiagnosticoenIndiagnop int
			select @ExisteDiagnosticoenIndiagnop = count(*) from INDIAGNOP where NUMINGRES = @Ingreso AND NUMEFOLIO  = @NUMEFOLIO AND CODDIAPRI = 1
			if @ExisteDiagnosticoenIndiagnop > 1 begin
				  raiserror('Contacte administrador sistemas: no puede llegar Grabar dos diagnosticos principales!',10,1)
	              rollback transaction
			end
end
GO
DISABLE TRIGGER [dbo].[DetectarDobleDiagnosticosEnLaHistoria]
    ON [dbo].[INDIAGNOP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirmación de estadificación TNM (Tumor-Nódulo-Metástasis) y estadio clínico del cáncer: 0=No confirmado, 1=Sí confirmado. Bit.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmación de campos T, N, M y Estadio --> 0=No, 1=Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio de cáncer secundario (obsoleto desde 10-02-2024 por autorización Product Owner Oncología). Valores: 0, A, B, C. Char(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo para almancear el Estadio 2 solo cuando el diagnostico sea de tipo Cáncer, valores 0-A-B-C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de cáncer por origen: 1=Primario, 2=Otro primario, 3=Primario desconocido, 4=Metástasis. Int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cáncer.  1 - Primario   2 - Otro Primario   3 - Primario desconocido   4 - Metástasis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de enfermedad huérfana/rara asociada al diagnóstico oncológico. FK a ADENFHUERFANAS. Int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion de la enfermedad huerfana asociada al Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadificación clínica del cáncer (0-99): incluye estadios 0-IV/4S, sin información (93), no aplica (98), desconocido (99). Referencia RIPS/OPS. Int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0=estadio clínico (ec) 0 (tumor in situ)  1=ec I o 1  2=ec IA o 1A  3=ec IA1  4=ec IA2  5=ec IB o 1b  6=ec IB1  7=ec IB2  8=ec IC o 1c  9=ec IS o 1s  10=ec II o 2  11=ec IIA o 2a  12=ec IIA1  13=ec IIA2  14=ec IIB o 2b  15=ec IIC o 2c  16=ec III o 3  17=ec IIIA o 3a  18= ec IIIB o 3b  19= ec IIIC o 3c  20=ec IV o 4  21= ec IVA o 4a  22=ec IVB o 4b  23=ec IVC o 4c  24= ec 4S (para neuroblastoma)  25= ec V o 5  26=Estadio IAB  55=Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorial durante el periodo de reporte  93= Sin información de estadificación en historia clínica  98=No Aplica (Es cáncer de piel basocelular, es cáncer hematológico o es cáncer en SNC, excepto neuroblastoma)  99=Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos"  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Metástasis (M) obsoleto del TNM, para cáncer confirmado (obsoleto desde 10-02-2024). Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''M'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Metástasis (M) del TNM, para cáncer confirmado (obsoleto desde 10-02-2024). Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''M'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'M1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Nódulos (N) linfáticos obsoleto del TNM, para cáncer confirmado (obsoleto desde 10-02-2024). Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Nódulos (N) linfáticos del TNM, para cáncer confirmado (obsoleto desde 10-02-2024). Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'N1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Tumor (T) primario obsoleto del TNM, para cáncer confirmado (obsoleto desde 10-02-2024). Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Tumor (T) primario del TNM, para cáncer confirmado (obsoleto desde 10-02-2024). Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'T1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de terminación/fin de tratamiento para diagnóstico de Leishmaniasis. DateTime.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Terminación Tratamiento para Leishmaniasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FECHLEISH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seguimiento de tratamiento interdisciplinario hospitalario según diagnóstico: salud mental (ansiedad, depresión, esquizofrenia, TDAH, SPA, bipolaridad) o enfermedades de notificación (hipotiroidismo congénito, sífilis gestacional/congénita, lepra). Estados: 1=En proceso, 2=Completado, 16-20=Motivos de no atención, 22=Sin dato. Int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el paciente con diagnostico de: Ansiedad, Depresión, Esquizofrenia, Deficit de atención, consumo SPA y Bipolaridad  1- El paciente está en procesio de atención por equipo interdisciplinario en el hospital ..  2- El paciente recibió atención por equipo interdisciplinario completo en el hospital...  16- El paciente no recibió atención por tener una tradición que se lo impide  17- No recibió atención por una condición de salud  18- No recibió atención por negación del usuario  20- No recibió atención por otras razones  22- Sin dato    Si el diagnostico Hipotiroidismo congenito, sifilis gestacional, sifilis congenita, lepra  1- El paciente recibe tratamiento en hospital...pero aún no ha terminado  2- El paciente recibió tratamiento en el hospital... y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18- No recibió tratamiento por negación del usuario  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TRATA4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de plantilla/template de diagnóstico para documentación clínica estandarizada. Text.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar el contenido de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador reservado para auditoría y trazabilidad de registros diagnósticos. Numeric(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del diagnóstico en el ingreso: 1=Activo, 2=Descartado. Int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la historia clínica donde se especificó el diagnóstico. Int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del folio de la historia clinica en donde se especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de asignación/registro del diagnóstico en el ingreso. DateTime.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Asignacion del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación clínica general, hallazgos y notas del diagnóstico. Char(250).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del diagnóstico según momento quirúrgico: PR=Preoperatorio, PO=Postoperatorio, PP=Pre y postoperatorio, HI=Histopatológico, NA=No aplica. Char(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico según confirmación: I=Impresión diagnóstica, C=Confirmado nuevo, R=Confirmado repetido. Char(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el diagnóstico corresponde a: I=Ingreso, E=Egreso, A=Ambos (ingreso y egreso). Char(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico principal (solo uno por ingreso): 0=Secundario, 1=Principal. Bit, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico Principal - Solo Aplica uno por Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud que emitió el diagnóstico. FK a INPROFSAL. Masked PII. Char(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 u nomenclatura clínica estándar. FK a INDIAGNOS. Masked PII. Char(4), clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento, identificación). FK a INPACIENT. Masked PII. Varchar(25), clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso/atención del paciente en el centro. FK a ADINGRESO. Char(10), clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (departamento, servicio, especialidad) donde se registró el diagnóstico. FK a INUNIFUNC. Char(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica, sede). FK a ADCENATEN. Char(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio secuencial del registro en historia clínica. Nchar(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica (papel, digital, híbrida). Char(9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos registrados por ingreso o atención clínica de cada paciente. Incluye el código CIE-10, tipo de diagnóstico, clasificación, observaciones clínicas y datos de estadificación oncológica (TNM y estadio), así como información de enfermedades huérfanas y leishmaniasis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOP';
