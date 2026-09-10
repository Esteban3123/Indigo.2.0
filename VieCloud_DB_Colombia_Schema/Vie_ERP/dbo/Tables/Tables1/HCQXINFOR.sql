CREATE TABLE [dbo].[HCQXINFOR] (
    [IDETIPHIS]                         CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                         CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                         CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                         CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                         CHAR (10)                                                                        NOT NULL,
    [CODDIAPRE]                         CHAR (4)                                                                         NULL,
    [CODDIAPOS]                         CHAR (4)                                                                         NULL,
    [CODPROSAL]                         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODSERIPS]                         CHAR (20)                                                                        NOT NULL,
    [TIPOANEST]                         CHAR (1)                                                                         NOT NULL,
    [FECHORINI]                         DATETIME                                                                         NOT NULL,
    [FECHORFIN]                         DATETIME                                                                         NOT NULL,
    [TIPHERCIR]                         CHAR (1)                                                                         NOT NULL,
    [SALACIRUG]                         CHAR (15)                                                                        NOT NULL,
    [URGECIRUG]                         BIT                                                                              NOT NULL,
    [CLASIFASA]                         CHAR (1)                                                                         NOT NULL,
    [PROFIANTI]                         BIT                                                                              NOT NULL,
    [PROTEIMPL]                         BIT                                                                              NOT NULL,
    [CXCADERAS]                         BIT                                                                              NOT NULL,
    [CXRODILLA]                         BIT                                                                              NOT NULL,
    [LAPAROTOM]                         BIT                                                                              NOT NULL,
    [FRACTABIE]                         BIT                                                                              NOT NULL,
    [CLAFRACAB]                         CHAR (4)                                                                         NULL,
    [DESVIAABO]                         VARCHAR (2000)                                                                   NULL,
    [DESHALLOP]                         VARCHAR (MAX)                                                                    NOT NULL,
    [DESPROCED]                         VARCHAR (MAX)                                                                    NOT NULL,
    [DESCOMPIC]                         VARCHAR (MAX)                                                                    NOT NULL,
    [CONTEOMAT]                         VARCHAR (200)                                                                    NULL,
    [DESCOMPRE]                         VARCHAR (200)                                                                    NULL,
    [DESCGASAS]                         VARCHAR (200)                                                                    NULL,
    [PREPATOLO]                         INT                                                                              NULL,
    [NUMCANMUE]                         INT                                                                              NULL,
    [OBSMUESTR]                         VARCHAR (2000)                                                                   NULL,
    [MATERADIC]                         VARCHAR (MAX)                                                                    NULL,
    [AUTO]                              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]                   INT                                                                              NULL,
    [TIPOINFORME]                       INT                                                                              NULL,
    [INFORMEPROINVASIVO]                VARCHAR (MAX)                                                                    NULL,
    [IMAGENPROINVASIVO]                 VARBINARY (MAX)                                                                  NULL,
    [DateOfAdministration]              DATETIME                                                                         NULL,
    [PerioperativeBleeding]             INT                                                                              NULL,
    [PerioperativeBleedingVolume]       NUMERIC (7, 2)                                                                   NULL,
    [MaterialCount]                     INT                                                                              NULL,
    [MaterialCountQuantity]             INT                                                                              NULL,
    [Compresses]                        INT                                                                              NULL,
    [CompressesQuantity]                INT                                                                              NULL,
    [Gauze]                             INT                                                                              NULL,
    [GauzeQuantity]                     INT                                                                              NULL,
    [ObservationsPerioperativeBleeding] VARCHAR (200)                                                                    NULL,
    [CODESPECI]                         CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCINFOQX1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC),
    CONSTRAINT [FK_HCQXINFOR_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCQXINFOR_INDIAGNOS] FOREIGN KEY ([CODDIAPRE]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCQXINFOR_INDIAGNOS1] FOREIGN KEY ([CODDIAPOS]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCQXINFOR_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCQXINFOR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCQXINFOR_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCQXINFOR_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCQXINFOR].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCQXINFOR].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_HCQXINFOR]
    ON [dbo].[HCQXINFOR]([IPCODPACI] ASC);


GO

CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL] 
   ON  [dbo].[HCQXINFOR] 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from [HCQXINFOR] hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.AUTO in (select AUTO from inserted)
end


END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad quirúrgica (Cirugía General, Ortopedia, etc.) con la cual se firmó y autorizó la historia clínica de la intervención. Referencia a INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código de la especialidad con la cual se firmó la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas del sangrado perioperatorio: notas adicionales sobre volumen, características, manejo hemostático durante y después del procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'ObservationsPerioperativeBleeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Sangrado perioperatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'ObservationsPerioperativeBleeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'ObservationsPerioperativeBleeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de gasas utilizadas en cirugía: 1=Completas, 2=Incompletas. Registro para saldo y control de materiales en el acto quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GauzeQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gasas Cantidad -- en esta columna se guardara la cantidad de gasas   1 COMPLETAS  2 INCOMPLETAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GauzeQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GauzeQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gasas quirúrgicas presentes en el procedimiento (Sí/No). Indicador binario para validar uso y conteo final de materiales en el campo operatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Gauze';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'GASAS -- en esta columna se guardaran los items de (SI O NO) del grupo de gasas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Gauze';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Gauze';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de compresas utilizadas: 1=Completas, 2=Incompletas. Control de materiales absorbentes en la intervención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CompressesQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'en esta columna se guardara la cantidad de compresas  valor de los item   1 -COMPLETAS  2-INCOMPLETAS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CompressesQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CompressesQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Compresas quirúrgicas: 1=No, 2=Sí, 3=No aplica. Indicador de presencia y estado de compresas en el conteo de materiales perioperatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Compresses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'en esta columna se guardara el valor de los items  del grupo compresas     1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Compresses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Compresses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad en conteo de material quirúrgico: 1=No, 2=Sí, 3=No aplica. Validación del registro completo de instrumentos y materiales utilizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCountQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CANTIDAD --- en esta columna se almacena la cantidad del (--Conteo material--)    1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCountQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCountQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conteo de material quirúrgico: 1=No, 2=Sí, 3=No aplica. Confirmación de que se realizó recuento exhaustivo de instrumentos, gasas y compresas al cierre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CONTEO MATERIAL -- en esta columna contendra el valor de los items valor de los item   1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen estimado de sangrado perioperatorio en mililitros (NUMERIC 7,2). Medida cuantitativa para evaluar pérdida hemática durante la cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleedingVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'VOLUMEN --- en esta columna se almacenara el volumen del grupo Sangrado Perioperatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleedingVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleedingVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sangrado perioperatorio: 1=No, 2=Sí, 3=No aplica. Indicador de presencia de sangrado significativo durante o inmediatamente después del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'SANGRADO PERIOPERATORIO -- en esta columna se almacenara el valor de los item   1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de administración de profilaxis antimicrobiana (DATETIME). Se habilita cuando se registra "Sí" en PROFIANTI para trazabilidad de antibióticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DateOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda hora y fecha del hora administración (se habilita cuando le dan si en el campo profilaxis con antimicrobianos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DateOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DateOfAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imagen binaria (VARBINARY MAX) del informe de procedimientos invasivos: fotografías, imagenología o documentación visual asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IMAGENPROINVASIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagen de Informe de procedimientos Invasivos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IMAGENPROINVASIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IMAGENPROINVASIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe completo en texto de procedimientos invasivos realizados durante la cirugía: cateterismos, drenajes, biopsias, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'INFORMEPROINVASIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Informe de procedimientos Invasivos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'INFORMEPROINVASIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'INFORMEPROINVASIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de informe: 1=Informe Quirúrgico (QX), 2=Informe No Quirúrgico. Clasificación del documento generado en Indigo Vie.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOINFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Informe:    1 - Informe QX  2 - Informe No QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOINFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOINFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la orden de servicio generada desde Control de Cuenta que incluye este servicio quirúrgico para facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Qx, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de auto-incremento (IDENTITY) de la tabla HCQXINFOR. Clave técnica única para cada registro de informe quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auto numerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Materiales adicionales quirúrgicos utilizados más allá de estándar: implantes especiales, suturas, drenajes, catéteres, biomateriales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MATERADIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Materiales Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MATERADIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MATERADIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de muestras anatómicas o patológicas enviadas a laboratorio: especímenes, tejidos extraídos, descripción de hallazgos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'OBSMUESTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'OBSMUESTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'OBSMUESTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de muestras tomadas: 1=Completo, 2=Incompleto. Validación de integridad de especímenes para estudio anatomopatológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMCANMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Muestras  1 COMPLETO  2 INCOMPLETO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMCANMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMCANMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de patologías intraoperatorias: 1=No, 2=Sí, 3=No aplica. Indicador de hallazgos patológicos inesperados encontrados durante cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PREPATOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presencia de Patologias  valor de los item   1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PREPATOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PREPATOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del grupo de gasas: notas sobre tipo, cantidad, estado, ubicación en el conteo final de materiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCGASAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'OBSERVACIONES --- en esta columna se guardara las observaciones del grupo gasas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCGASAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCGASAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del grupo de compresas: detalles sobre cantidad, integridad, localización en el recuento perioperatorio de materiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'en este columna se guardar las OBSERVACIONES del grupo COMPRESAS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del conteo de material quirúrgico: notas sobre discrepancias, materiales adicionales, estado de instrumental al cierre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CONTEOMAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En esta columna contendra las observaciones del grupo de conteo de material', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CONTEOMAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CONTEOMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicaciones intraoperatorias o perioperatorias: hemorragia, infección, lesiones iatrogénicas, eventos adversos durante la intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de procedimientos y técnicas quirúrgicas realizadas: pasos técnicos, abordajes, maniobras, hemostasia efectuada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgos operatorios: descripción de anatomía encontrada, patología intraoperatoria, extensión de la lesión, estado de órganos y estructuras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESHALLOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hallazgo Operativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESHALLOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESHALLOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de abordaje quirúrgico utilizada: incisión principal, localización anatómica, técnica de acceso (abierta, laparoscópica, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESVIAABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Via de Abordaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESVIAABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESVIAABO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de fractura abierta (Gustilo-Anderson): I, II, IIIA, IIIB, IIIC. Gradación de severidad para fracturas expuestas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLAFRACAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion Fractura Abierta:  I  II  IIIA  IIIB  IIIC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLAFRACAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLAFRACAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fractura abierta presente (Sí/No, BIT). Indicador de fractura con solución de continuidad de piel y comunicación con foco de fractura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FRACTABIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fractura Abierta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FRACTABIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FRACTABIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Laparotomía realizada (Sí/No, BIT). Indicador de incisión abdominal para exploración o tratamiento de patología abdominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'LAPAROTOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laparotomia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'LAPAROTOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'LAPAROTOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cirugía de rodilla realizada (Sí/No, BIT). Indicador de procedimiento quirúrgico sobre articulación tibiofemoral o patelofemoral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXRODILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CX Rodilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXRODILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXRODILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cirugía de cadera realizada (Sí/No, BIT). Indicador de intervención quirúrgica sobre articulación coxofemoral, prótesis o traumatología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXCADERAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CX Cadera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXCADERAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXCADERAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prótesis o implante utilizado (Sí/No, BIT). Indicador de inserción de dispositivo protésico, osteosíntesis o material de reconstrucción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROTEIMPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Protesis / Implante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROTEIMPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROTEIMPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profilaxis con antimicrobianos realizada (Sí/No, BIT). Registro de administración de antibiótico preventivo intraoperatorio. Habilita DateOfAdministration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROFIANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profilaxis con antimicrobianos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROFIANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROFIANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación ASA (American Society of Anesthesiologists): 1=Sano, 2=Enfermedad leve, 3=Enfermedad grave, 4=Enfermedad grave peligro vida, 5=Moribundo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLASIFASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion ASA(Asociacion Quirurgica Estadounidense):  1  2  3  4  5 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLASIFASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLASIFASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cirugía urgente (Sí/No, BIT). Indicador de procedimiento de carácter urgente vs electivo, para priorización y gestión quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'URGECIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'URGECIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'URGECIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificador de la sala quirúrgica donde se realizó la intervención. Trazabilidad de infraestructura y recursos utilizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'SALACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'SALACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'SALACIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de herida quirúrgica (clasificación clínica): 1=Limpia, 2=Limpia-contaminada, 3=Contaminada, 4=Sucia (vigente desde 04/12/2024). Riesgo infeccioso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPHERCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Herida:  1. Limpia   2. Limpia Contaminada   3. Contaminada   4. Sucia e Infectada   5. Sucia   6. Infectada   7. No Aplica   -----   4/12/2024
 apartir de esa fecha solo se guardara de la siguiente  manera    1. Limpia
2. Limpia Contaminada     3. Contaminada     4. Sucia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPHERCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPHERCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final de la intervención quirúrgica (DATETIME). Marca de cierre y fin de anestesia, tiempo quirúrgico total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de la intervención quirúrgica (DATETIME). Marca de inicio de anestesia y comienzo del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de anestesia administrada: 1=Local, 2=Regional, 3=General, 4=Combinada, 5=No aplica, 6=Raquídea, 7=Peridural, 8=Caudal, 9=Sedación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOANEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Anestesia:  1. Local  2. Regional  3. General  4. Combinada  5. No Aplica  6. Raquídea  7. Peridural  8. Caudal  9. Sedación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOANEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOANEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento y servicio CUPS (Cirugía Principal): identificador del procedimiento quirúrgico principal facturado según RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios (Cirugia Principal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (Cirujano Principal) que ejecuta la intervención. Referencia ofuscada a INPROFSAL para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud (Cirujano Principal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico post-operatorio (CIE-10 o CUPS). Referencia a INDIAGNOS para diagnóstico confirmado después de la cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico Pos-Operatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico pre-operatorio (CIE-10 o CUPS). Referencia a INDIAGNOS para diagnóstico de ingreso antes del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico Pre-Operatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio quirúrgico, UCI, piso, etc.). Referencia a INUNIFUNC para organización administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica, sede) donde se realiza la cirugía. Localización de prestación de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente a la institución. Referencia FK a ADINGRESO vinculando historia clínica con admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, documento de identificación). PII ofuscado en búsquedas, referencia FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del informe quirúrgico. Identificador secuencial del documento dentro de la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica: tipo de documento generado en el sistema Indigo Vie (ej: QXINFORME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe quirúrgico de procedimientos realizados durante una intervención de cirugía. Registra los datos clínicos, técnicos y de seguridad de cada acto quirúrgico: diagnósticos pre y posoperatorios, tipo de anestesia, sala y hora de la cirugía, complicaciones, conteo de materiales, sangrado perioperatorio y hallazgos del procedimiento, vinculados al paciente y al ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR';
