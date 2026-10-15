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
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_ADINGRESO];


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INDIAGNOS1];


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INESPECIA];


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INPACIENT];


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INPROFSAL];


GO
ALTER TABLE [dbo].[HCQXINFOR] NOCHECK CONSTRAINT [FK_HCQXINFOR_INUNIFUNC];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el código de la especialidad con la cual se firmó la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de Sangrado perioperatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'ObservationsPerioperativeBleeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gasas Cantidad -- en esta columna se guardara la cantidad de gasas   1 COMPLETAS  2 INCOMPLETAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GauzeQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'GASAS -- en esta columna se guardaran los items de (SI O NO) del grupo de gasas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Gauze';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en esta columna se guardara la cantidad de compresas  valor de los item   1 -COMPLETAS  2-INCOMPLETAS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CompressesQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en esta columna se guardara el valor de los items  del grupo compresas     1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'Compresses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CANTIDAD --- en esta columna se almacena la cantidad del (--Conteo material--)    1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCountQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CONTEO MATERIAL -- en esta columna contendra el valor de los items valor de los item   1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MaterialCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VOLUMEN --- en esta columna se almacenara el volumen del grupo Sangrado Perioperatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleedingVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SANGRADO PERIOPERATORIO -- en esta columna se almacenara el valor de los item   1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PerioperativeBleeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se guarda hora y fecha del hora administración (se habilita cuando le dan si en el campo profilaxis con antimicrobianos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DateOfAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagen de Informe de procedimientos Invasivos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IMAGENPROINVASIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Informe de procedimientos Invasivos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'INFORMEPROINVASIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Informe:    1 - Informe QX  2 - Informe No QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOINFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Qx, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto numerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Materiales Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'MATERADIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'OBSMUESTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Muestras  1 COMPLETO  2 INCOMPLETO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMCANMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presencia de Patologias  valor de los item   1 - no  2-si  3 -no aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PREPATOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OBSERVACIONES --- en esta columna se guardara las observaciones del grupo gasas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCGASAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en este columna se guardar las OBSERVACIONES del grupo COMPRESAS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En esta columna contendra las observaciones del grupo de conteo de material', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CONTEOMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESCOMPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hallazgo Operativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESHALLOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Via de Abordaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'DESVIAABO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion Fractura Abierta:  I  II  IIIA  IIIB  IIIC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLAFRACAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fractura Abierta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FRACTABIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laparotomia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'LAPAROTOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CX Rodilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXRODILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CX Cadera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CXCADERAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Protesis / Implante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROTEIMPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profilaxis con antimicrobianos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'PROFIANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion ASA(Asociacion Quirurgica Estadounidense):  1  2  3  4  5 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CLASIFASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'URGECIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'SALACIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Herida:  1. Limpia   2. Limpia Contaminada   3. Contaminada   4. Sucia e Infectada   5. Sucia   6. Infectada   7. No Aplica   -----   4/12/2024
 apartir de esa fecha solo se guardara de la siguiente  manera    1. Limpia
2. Limpia Contaminada     3. Contaminada     4. Sucia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPHERCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'FECHORINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Anestesia:  1. Local  2. Regional  3. General  4. Combinada  5. No Aplica  6. Raquídea  7. Peridural  8. Caudal  9. Sedación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'TIPOANEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios (Cirugia Principal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud (Cirujano Principal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico Pos-Operatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico Pre-Operatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODDIAPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXINFOR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

