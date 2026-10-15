

CREATE PROCEDURE [dbo].[SP_RolePermissionsForm]
(
    -- Add the parameters for the stored procedure here
    @RoleCode varchar(10),  
	@IdForm VARCHAR(5) --es nulo o vacio si se llama desde el formulario de rol en el hist

)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	DECLARE @IdErpForm VARCHAR(5)
	DECLARE @Tabla as TABLE
				(
					codigorol varchar(10),
					indidmenu VARCHAR(5),
					ioptguard TINYINT,
					ioptconsu TINYINT,
					ioptdisen TINYINT,
					ioptactua TINYINT,
					ioptelimi TINYINT,
					ioptnaveg TINYINT,
					ioptconfi TINYINT,
					ioptanula TINYINT,
					ioptimpri TINYINT,
					igricrear TINYINT,
					igrimodif TINYINT,
					igrielimi TINYINT,
					ioptvisib TINYINT,
					customizable TINYINT,
					PreAltaHospitalaria  TINYINT,
					SoporteVieliberarCamaDobleEstancia   TINYINT, 
					SoporteVieEliminarRegistroEgreso   TINYINT
				
				)

	--SELECT @IdErpForm = IdErpForm from [Security].[FormRelationship] where IdHisForm = @IdForm
	SELECT @IdErpForm = @IdForm
	IF @IdErpForm IS NULL
	BEGIN
		SELECT @RoleCode AS codigorol, @IdForm AS indidmenu
				,CAST(0 AS BIT) AS ioptguard
				,CAST(0 AS BIT) AS ioptconsu
				,CAST(0 AS BIT) AS ioptdisen
				,CAST(0 AS BIT) AS ioptactua
				,CAST(0 AS BIT) AS ioptelimi
				,CAST(0 AS BIT) AS ioptnaveg
				,CAST(0 AS BIT) AS ioptconfi
				,CAST(0 AS BIT) AS ioptanula
				,CAST(0 AS BIT) AS ioptimpri
				,CAST(0 AS BIT) AS igricrear
				,CAST(0 AS BIT) AS igrimodif
				,CAST(0 AS BIT) AS igrielimi
				,CAST(0 AS BIT) AS ioptvisib
				,CAST(0 AS BIT) AS customizable
				,CAST(0 AS BIT) AS PreAltaHospitalaria
				,CAST(0 AS BIT) AS SoporteVieliberarCamaDobleEstancia    
				,CAST(0 AS BIT) AS SoporteVieEliminarRegistroEgreso   
	END
	ELSE
	BEGIN
			--IF @IdErpForm = '828'
			--BEGIN
			--	select @IdErpForm = '2708' --
			--	set @IdForm = @IdErpForm
			--END
			--ELSE
			--BEGIN
				IF @IdErpForm = '001'
				BEGIN
					select @IdErpForm = '1629' 
					set @IdForm = @IdErpForm
				END
				ELSE
				BEGIN
					IF @IdErpForm = '079'
					BEGIN
						select @IdErpForm = '2700'
						set @IdForm = @IdErpForm
					END
					ELSE
					BEGIN
						if  @IdErpForm IN ('904','965','475','808','2291','2629','1665','828','005','100','101', '109', '113', '114', '123','127', '138', '143', '167', '186', '193', '196', '197', '205','211', '215', '216', '218', '219', '221', '225', '258', '259', '309', '311', '314', '326', '327','401', '402', '403', '404','412', '413', '415', '418'
						, '419', '420', '425', '426', '427', '428', '429', '430', '431', '436', '437', '438', '439', '440', '441', '442', '443', '444', '446', '470','810', '815', '855', '888', '901', '953', '954', '959', '960', '961', '967', '971', '976', '977'
						, '978', '979', '980', '982', '983', '984', '985', '986', '987', '988', '989','990','991', '992', '993', '994', '995', '996', '997', '998','999','840','320'
						,'206','406','407','408','409','410','463','464','465','466','468','330','331','476', '323', '322', '324', '325', '421', '432', '433', '434', '435', '445', '455', '908', '909', '910', '818'
						) OR (SUBSTRING(@IdErpForm,1,1) = '0')
						BEGIN
							select @IdErpForm = IdErpForm from Security.FormRelationshipInt where IdHisForm = @IdErpForm
							print @IdErpForm
							set @IdForm = @IdErpForm
						END
					END
				END
		--	END
			
				;WITH Permission AS
				(
				SELECT
				PR.[Action], PR.ActionValue 
				FROM Security.RollInt R
				INNER JOIN Security.PermissionRollInt PR ON PR.IdRoll = R.Id AND PR.IdForm = @IdErpForm AND PR.[Action] IN ('2','40','130','3','1','131','7','8','23','132','133','134','41','11','146','148','149')
				WHERE R.RollCode = @RoleCode 
				)
				INSERT INTO @Tabla
				SELECT
					codigorol = @RoleCode,
					indidmenu = @IdForm,
					ioptguard =      MAX(CASE WHEN P.[Action] = '2' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptconsu =      MAX(CASE WHEN P.[Action] = '40' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptdisen =      MAX(CASE WHEN P.[Action] = '130' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptactua =      MAX(CASE WHEN P.[Action] = '3' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptelimi =      MAX(CASE WHEN P.[Action] = '1' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptnaveg =      MAX(CASE WHEN P.[Action] = '131' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptconfi =      MAX(CASE WHEN P.[Action] = '7' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptanula =      MAX(CASE WHEN P.[Action] = '8' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptimpri =      MAX(CASE WHEN P.[Action] = '23' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					igricrear =      MAX(CASE WHEN P.[Action] = '132' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					igrimodif =      MAX(CASE WHEN P.[Action] = '133' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					igrielimi =      MAX(CASE WHEN P.[Action] = '134' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					ioptvisib =      MAX(CASE WHEN P.[Action] = '41' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					customizable =   MAX(CASE WHEN P.[Action] = '11' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					PreAltaHospitalaria =  MAX(CASE WHEN P.[Action] = '146' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					SoporteVieliberarCamaDobleEstancia =  MAX(CASE WHEN P.[Action] = '148' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					SoporteVieEliminarRegistroEgreso =  MAX(CASE WHEN P.[Action] = '149' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END)
				FROM Permission AS P;

				SELECT 
					codigorol,
					indidmenu,
					ioptguard = CAST(isnull(ioptguard, 0) AS BIT),
					ioptconsu = CAST(isnull(ioptconsu, 0) AS BIT),
					ioptdisen = CAST(isnull(ioptdisen, 0) AS BIT),
					ioptactua = CAST(isnull(ioptactua, 0) AS BIT),
					ioptelimi = CAST(isnull(ioptelimi, 0) AS BIT),
					ioptnaveg = CAST(isnull(ioptnaveg, 0) AS BIT),
					ioptconfi = CAST(isnull(ioptconfi, 0) AS BIT),
					ioptanula = CAST(isnull(ioptanula, 0) AS BIT),
					ioptimpri = CAST(isnull(ioptimpri, 0) AS BIT),
					igricrear = CAST(isnull(igricrear, 0) AS BIT),
					igrimodif = CAST(isnull(igrimodif, 0) AS BIT),
					igrielimi = CAST(isnull(igrielimi, 0) AS BIT),
					ioptvisib = CAST(isnull(ioptvisib, 0) AS BIT),
					customizable = CAST(isnull(customizable, 0) AS BIT),
					PreAltaHospitalaria = CAST(isnull(PreAltaHospitalaria, 0) AS BIT),
					SoporteVieliberarCamaDobleEstancia = CAST(isnull(SoporteVieliberarCamaDobleEstancia, 0) AS BIT),
					SoporteVieEliminarRegistroEgreso = CAST(isnull(SoporteVieEliminarRegistroEgreso, 0) AS BIT)
			FROM @Tabla
	END
END