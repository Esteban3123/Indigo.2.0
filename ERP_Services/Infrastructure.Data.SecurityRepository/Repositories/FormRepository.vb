'***********************************************************************
' Assembly         : Infraestructura.Datos.RepositorioSeguridad
' Author           : Jhon Tovar
' Last Modified On : 2022-02-25
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Configuration
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Interface
Imports Infrastructure.Data.Base

''' <summary>
''' Esta clase contiene cada uno de los metodos y funciones que no son comunes dentro del repositorio generico ubicado
''' en Infraestructura.Base	 ademas implementa de la interfaz ubicada en la capa de Dominio.Seguridad. Esta clase es la encargada 
''' realizar las respectivas consultas de formularios para el menu ubicados en roles y usuarios para generar el treeVeiew
''' </summary>
Public Class FormRepository
    Implements IFormRepository

#Region "Form"

    ''' <summary>
    ''' Consulta de lista de formularios, relacionado, con acciones y activo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFormModule() As List(Of VieForm) Implements IFormRepository.ListFormModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim listForms As List(Of VieForm) = New List(Of VieForm)()
            Dim dt1 As DataTable = Nothing

            dt1 = conx.ExecuteCommand_Data("SELECT DISTINCT PC.Id IdProduct,PC.ProductName, 
	            M.Id IdModule, M.Name ModuleName, 
	            T.Id IdTitle, T.Name TitleName, ISNULL(Mt.[Order],99) TitleOrder,
	            F.Id IdForm,F.Name FormName, MF.FormOrder, ISNULL(F.PrintEvents,'') PrintEvents, 
                F.HasSequence, F.IsNativeForm, F.HasForm,ISNULL(F.ClassName,'') ClassName,
                ISNULL(f.AssemblyName,'') AssemblyName,f.HandlesMassiveConfirm, ISNULL(F.SequenceModule, '') SequenceModule,
	            A.Id IdAction,A.Name ActionName
            FROM Security.ProductCatalog PC
            INNER JOIN [Security].[ProductModule] PM ON PC.Id = PM.IdProduct
            INNER JOIN [Security].[Module] M ON PM.IdModule = M.Id AND M.State = 1
            INNER JOIN [Security].[ModuleForm] MF ON M.Id = MF.IdModule 
            INNER JOIN [Security].[Title] T ON MF.IdTitle = T.Id
            INNER JOIN [Security].[Form] F ON MF.IdForm = F.Id AND F.State = 1 AND (F.IsNativeForm <> 1 OR F.ClassName IS NOT NULL)
			LEFT JOIN [Security].[FormAction] FA ON F.Id = FA.IdForm
            LEFT JOIN [Security].[Action] A ON FA.IdAction = A.Id AND A.State = 1
            LEFT JOIN [Security].[ModuleTitle] MT ON MF.IdModule = MT.IdModule AND MF.IdTitle = MT.IdTitle
            ORDER BY  IdProduct,IdModule,IdForm")


            Dim lastProduct As VieGroup = Nothing
            Dim lastModule As VieModule = Nothing
            Dim lastForm As VieForm = Nothing
            Dim idProduct As String
            Dim idModule As String
            Dim idForm As String

            For index As Integer = 0 To dt1.Rows.Count() - 1
                idProduct = dt1.Rows(index)("IdProduct")
                idModule = dt1.Rows(index)("IdModule")
                idForm = dt1.Rows(index)("IdForm")

                If lastForm IsNot Nothing AndAlso idForm <> lastForm.Id Then
                    listForms.Add(lastForm)
                End If

                If lastForm Is Nothing OrElse idForm <> lastForm.Id Then
                    lastForm = New VieForm() With {
                    .Id = idForm,
                    .AssemblyName = dt1.Rows(index)("AssemblyName"),
                    .ClassName = dt1.Rows(index)("ClassName"),
                    .Name = dt1.Rows(index)("FormName"),
                    .Order = dt1.Rows(index)("FormOrder"),
                    .HandlesMassiveConfirm = dt1.Rows(index)("HandlesMassiveConfirm"),
                    .HasForm = dt1.Rows(index)("HasForm"),
                    .HasSequence = dt1.Rows(index)("HasSequence"),
                    .IsNativeForm = dt1.Rows(index)("IsNativeForm"),
                    .PrintEvents = dt1.Rows(index)("PrintEvents"),
                    .GroupForms = dt1.Rows(index)("ProductName"),
                    .Type = dt1.Rows(index)("IdTitle"),
                    .TypeName = dt1.Rows(index)("TitleName"),
                    .TypeOrder = dt1.Rows(index)("TitleOrder"),
                    .IdModuleSource = idProduct,
                    .SequenceModule = dt1.Rows(index)("SequenceModule"),
                    .Module = New VieModule() With {.Id = idModule, .Name = dt1.Rows(index)("ModuleName"), .ProductCatalogId = idProduct},
                    .Permissions = New List(Of ViePermission)()
                }
                End If
                If (Not dt1.Rows(index).IsNull("IdAction")) Then
                    Dim action = New ViePermission() With {.Id = dt1.Rows(index)("IdAction"), .Name = dt1.Rows(index)("ActionName")}
                    lastForm.Permissions.Add(action)
                End If
            Next
            If lastForm IsNot Nothing Then
                listForms.Add(lastForm)
            End If
            Return listForms
        End Using
    End Function

    Public Function SaveForm(form As VieDBForm) As Boolean Implements IFormRepository.SaveForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO [Security].Form (Id,Name,PrintEvents,HasSequence,IsNativeForm,HasForm,ClassName,AssemblyName,HandlesMassiveConfirm,SequenceModule,State)
                        VALUES(@Id,@Name,@PrintEvents,@HasSequence,@IsNativeForm,@HasForm,@ClassName,@AssemblyName,@HandlesMassiveConfirm,@SequenceModule,1)"

            conx.AddParam("Id", SqlDbType.Int, form.IdForm)
            conx.AddParam("Name", SqlDbType.VarChar, form.FormName)
            conx.AddParam("PrintEvents", SqlDbType.VarChar, form.PrintEvents)
            conx.AddParam("HasSequence", SqlDbType.Bit, form.HasSequence)
            conx.AddParam("IsNativeForm", SqlDbType.Bit, form.IsNativeForm)
            conx.AddParam("HasForm", SqlDbType.Bit, form.HasForm)
            conx.AddParam("ClassName", SqlDbType.VarChar, form.ClassName)
            conx.AddParam("AssemblyName", SqlDbType.VarChar, form.AssemblyName)
            conx.AddParam("HandlesMassiveConfirm", SqlDbType.Bit, form.HandlesMassiveConfirm)
            conx.AddParam("SequenceModule", SqlDbType.VarChar, form.SequenceModule)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function UpdateForm(form As VieDBForm) As Boolean Implements IFormRepository.UpdateForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE [Security].Form SET 
                        [Name]					=@Name
                        ,PrintEvents			=@PrintEvents
                        ,HasSequence			=@HasSequence
                        ,IsNativeForm			=@IsNativeForm
                        ,HasForm				=@HasForm
                        ,ClassName				=@ClassName
                        ,AssemblyName			=@AssemblyName
                        ,HandlesMassiveConfirm	=@HandlesMassiveConfirm
                        ,SequenceModule			=@SequenceModule
                        WHERE Id = @Id"

            conx.AddParam("Id", SqlDbType.Int, form.IdForm)
            conx.AddParam("Name", SqlDbType.VarChar, form.FormName)
            conx.AddParam("PrintEvents", SqlDbType.VarChar, form.PrintEvents)
            conx.AddParam("HasSequence", SqlDbType.Bit, form.HasSequence)
            conx.AddParam("IsNativeForm", SqlDbType.Bit, form.IsNativeForm)
            conx.AddParam("HasForm", SqlDbType.Bit, form.HasForm)
            conx.AddParam("ClassName", SqlDbType.VarChar, form.ClassName)
            conx.AddParam("AssemblyName", SqlDbType.VarChar, form.AssemblyName)
            conx.AddParam("HandlesMassiveConfirm", SqlDbType.Bit, form.HandlesMassiveConfirm)
            conx.AddParam("SequenceModule", SqlDbType.VarChar, form.SequenceModule)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function GetForm(IdForm As Integer) As VieDBForm Implements IFormRepository.GetForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim res As VieDBForm = New VieDBForm()

            Dim query = "SELECT Id,[Name],PrintEvents,HasSequence,IsNativeForm,HasForm,ClassName,AssemblyName,HandlesMassiveConfirm,SequenceModule,[State] FROM [Security].Form WHERE Id = @Id"

            conx.AddParam("Id", SqlDbType.Int, IdForm)
            Dim dt As DataTable = conx.ExecuteCommandParams_Data(query)

            If dt.Rows.Count > 0 Then
                res.IdForm = CInt(dt.Rows(0)("Id"))
                res.FormName = dt.Rows(0)("Name").ToString()
                res.PrintEvents = dt.Rows(0)("PrintEvents").ToString()
                res.HasSequence = Convert.ToByte(dt.Rows(0)("HasSequence"))
                res.IsNativeForm = Convert.ToByte(dt.Rows(0)("IsNativeForm"))
                res.HasForm = Convert.ToByte(dt.Rows(0)("HasForm"))
                res.ClassName = dt.Rows(0)("ClassName").ToString()
                res.AssemblyName = dt.Rows(0)("AssemblyName").ToString()
                res.HandlesMassiveConfirm = Convert.ToByte(dt.Rows(0)("HandlesMassiveConfirm"))
                res.SequenceModule = dt.Rows(0)("SequenceModule").ToString()
                res.State = Convert.ToByte(dt.Rows(0)("State"))
            End If

            Return res
        End Using
    End Function

    Public Function DeleteForm(IdForm As Integer) As Boolean Implements IFormRepository.DeleteForm
        Dim Result As Boolean
        Dim query As String
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Try
                If conx.sqlWebConection.State = ConnectionState.Closed Then
                    conx.sqlWebConection.Open()
                End If

                conx.InTransaction = True
                conx.IndigoTransaction = conx.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Eliminar Relacion Formulario")
                conx.AddParam("Id", SqlDbType.Int, IdForm)

                query = "DELETE FROM Security.FormAction WHERE IdForm = @Id"
                conx.ExecuteCommandParams(query, False)

                query = "DELETE FROM Security.ModuleForm WHERE IdForm = @Id"
                conx.ExecuteCommandParams(query, False)

                query = "DELETE FROM Security.Form WHERE Id = @Id"
                conx.ExecuteCommandParams(query, False)

                conx.IndigoTransaction.Commit()
                Result = True
            Catch ex As Exception
                conx.IndigoTransaction.Rollback()
                Result = False
            Finally
                conx.sqlWebConection.Close()
            End Try
        End Using

        Return Result
    End Function

    Public Function ChangeStateForm(IdForm As Integer, state As Byte) As Boolean Implements IFormRepository.ChangeStateForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.Form SET State = @State WHERE Id = @Id"

            conx.AddParam("Id", SqlDbType.Int, IdForm)
            conx.AddParam("State", SqlDbType.Bit, state)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function ListVieDBFormsImport() As List(Of VieDBForm) Implements IFormRepository.ListVieDBFormsImport
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim listForms As List(Of VieDBForm) = New List(Of VieDBForm)()

            Dim query = "SELECT A.Id IdForm,A.[Name] FormName,PrintEvents,HasSequence,IsNativeForm,HasForm,ClassName,AssemblyName,HandlesMassiveConfirm,SequenceModule,A.[State],B.Id,B.IdAction,C.Name
                        FROM [Security].Form A
                        LEFT JOIN [Security].FormAction B ON A.Id = B.IdForm
                        LEFT JOIN [Security].[Action] C ON B.IdAction = C.Id AND C.State = 1
                        WHERE A.State = 1 AND LEN(A.ClassName)> 0 ORDER BY A.Id"
            Dim dt As DataTable = conx.ExecuteCommandParams_Data(query)

            Dim lastForm As VieDBForm = Nothing
            Dim idForm As String
            For index As Integer = 0 To dt.Rows.Count() - 1
                idForm = CInt(dt.Rows(index)("IdForm"))
                If lastForm IsNot Nothing AndAlso idForm <> lastForm.IdForm Then
                    listForms.Add(lastForm)
                End If
                If lastForm Is Nothing OrElse idForm <> lastForm.IdForm Then
                    lastForm = New VieDBForm() With {
                .IdForm = idForm,
                .FormName = dt.Rows(index)("FormName").ToString(),
                .PrintEvents = dt.Rows(index)("PrintEvents").ToString(),
                .HasSequence = Convert.ToByte(dt.Rows(index)("HasSequence")),
                .IsNativeForm = Convert.ToByte(dt.Rows(index)("IsNativeForm")),
                .HasForm = Convert.ToByte(dt.Rows(index)("HasForm")),
                .ClassName = dt.Rows(index)("ClassName").ToString(),
                .AssemblyName = dt.Rows(index)("AssemblyName").ToString(),
                .HandlesMassiveConfirm = Convert.ToByte(dt.Rows(index)("HandlesMassiveConfirm")),
                .SequenceModule = dt.Rows(index)("SequenceModule").ToString(),
                .State = Convert.ToByte(dt.Rows(index)("State"))
                }
                End If
                If (Not dt.Rows(index).IsNull("Id")) Then
                    Dim FormAction = New FormAction() With {.Id = dt.Rows(index)("Id"), .IdAction = dt.Rows(index)("IdAction"), .IdForm = idForm}
                    FormAction.Action = New Action() With {.IdAction = dt.Rows(index)("IdAction"), .ActionName = dt.Rows(index)("Name")}
                    lastForm.ListFormAction.Add(FormAction)
                End If
            Next

            If lastForm IsNot Nothing Then
                listForms.Add(lastForm)
            End If

            Return listForms
        End Using
    End Function

#End Region

#Region "FormAction"
    Public Function ListFormActionByForm(IdForm As Integer) As List(Of FormAction) Implements IFormRepository.ListFormActionByForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim listFormAction As List(Of FormAction) = New List(Of FormAction)()
            Dim dt As DataTable = Nothing

            Dim query = "SELECT DISTINCT ISNULL(B.Id,0) Id, ISNULL(B.IdForm,0) IdForm,A.Id IdAction,A.Name,IIF(b.Id IS NOT NULL, 1 , 0) [State]
                        FROM  [Security].action A
                        LEFT JOIN [Security].FormAction B ON A.Id = B.IdAction AND B.IdForm = @Id
                        WHERE A.[State] = 1 
                        ORDER BY A.NAME"

            conx.AddParam("Id", SqlDbType.Int, IdForm)
            dt = conx.ExecuteCommandParams_Data(query)

            Dim formAction As FormAction = Nothing
            Dim action As Action = Nothing

            For index As Integer = 0 To dt.Rows.Count() - 1
                action = New Action() With {
                .IdAction = CInt(dt.Rows(index)("IdAction")),
                .ActionName = dt.Rows(index)("Name").ToString(),
                .State = Convert.ToByte(dt.Rows(index)("State"))
            }
                formAction = New FormAction() With {
                .Id = CInt(dt.Rows(index)("Id")),
                .IdForm = IdForm,
                .IdAction = action.IdAction,
                .Action = action
            }
                listFormAction.Add(formAction)

            Next
            Return listFormAction
        End Using
    End Function

    Public Function SaveFormAction(form As FormAction) As Boolean Implements IFormRepository.SaveFormAction
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO [Security].FormAction (IdForm,IdAction)
                        VALUES(@IdForm,@IdAction)"

            conx.AddParam("IdForm", SqlDbType.Int, form.IdForm)
            conx.AddParam("IdAction", SqlDbType.Int, form.IdAction)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function DeleteFormAction(form As FormAction) As Boolean Implements IFormRepository.DeleteFormAction
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "DELETE FROM Security.FormAction WHERE  IdAction = @IdAction AND IdForm = @IdForm"

            conx.AddParam("IdForm", SqlDbType.Int, form.IdForm)
            conx.AddParam("IdAction", SqlDbType.Int, form.IdAction)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

#End Region

End Class
