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

''' <summary>
''' Esta clase contiene cada uno de los metodos y funciones que no son comunes dentro del repositorio generico ubicado
''' en Infraestructura.Base	 ademas implementa de la interfaz ubicada en la capa de Dominio.Seguridad. Esta clase es la encargada 
''' realizar las respectivas consultas de formularios para el menu ubicados en roles y usuarios para generar el treeVeiew
''' </summary>
Public Class ModuleRepository
    Implements IModuleRepository

#Region "Module"
    Public Function ListVieModule() As List(Of VieModule) Implements IModuleRepository.ListVieModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim listModules As List(Of VieModule) = New List(Of VieModule)()
            Dim dt1 As DataTable = Nothing

            dt1 = conx.ExecuteCommand_Data("SELECT DISTINCT PC.Id IdProduct,PC.ProductName, 
	            M.Id IdModule, M.Name ModuleName
            FROM 
            Security.ProductCatalog PC
            INNER JOIN [Security].[ProductModule] PM ON PC.Id = PM.IdProduct
            INNER JOIN [Security].[Module] M ON PM.IdModule = M.Id AND M.State = 1
            ORDER BY  IdProduct,IdModule")

            For index As Integer = 0 To dt1.Rows.Count() - 1
                Dim vieModule = New VieModule() With {
                .Id = dt1.Rows(index)("IdModule"),
                .Name = dt1.Rows(index)("ModuleName"),
                .Description = dt1.Rows(index)("ProductName")
            }

                listModules.Add(vieModule)
            Next

            Return listModules
        End Using
    End Function

    Public Function SaveModule(modules As Modules, companyCode As String) As Boolean Implements IModuleRepository.SaveModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO Security.Module(Id,Name,Description,State) VALUES(@IdModule,@ModuleName,@Description,@State)"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("ModuleName", SqlDbType.VarChar, modules.ModuleName)
            conx.AddParam("Description", SqlDbType.VarChar, modules.Description)
            conx.AddParam("State", SqlDbType.Bit, 1)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function UpdateModule(modules As Modules, companyCode As String) As Boolean Implements IModuleRepository.UpdateModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.Module SET Name = @ModuleName, Description = @Description WHERE Id = @IdModule"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("ModuleName", SqlDbType.VarChar, modules.ModuleName)
            conx.AddParam("Description", SqlDbType.VarChar, modules.Description)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function GetModule(IdModule As Integer, companyCode As String) As Modules Implements IModuleRepository.GetModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim res As Modules = New Modules()
            Dim productCatalog As ProductCatalog
            Dim query = String.Format("SELECT A.Id,A.Name,A.Description,A.State,
						ISNULL(C.Id,0) IdProductCatalog,C.ProductName
					FROM Security.Module  A
					LEFT JOIN Security.ProductModule B ON A.Id  = B.IdModule
					LEFT JOIN Security.ProductCatalog C ON B.IdProduct = C.Id
					WHERE A.Id = {0}", IdModule)
            Dim dt As DataTable = conx.ExecuteCommand_Data(query)
            If dt.Rows.Count > 0 Then
                res.IdModule = CInt(dt.Rows(0)("Id"))
                res.ModuleName = dt.Rows(0)("Name").ToString()
                res.Description = dt.Rows(0)("Description").ToString()
                res.State = Convert.ToByte(dt.Rows(0)("State"))

                productCatalog = New ProductCatalog() With {
            .IdProduct = CInt(dt.Rows(0)("IdProductCatalog")),
            .ProductName = dt.Rows(0)("ProductName").ToString()
            }
                res.ProductCatalog = productCatalog
            End If

            Return res
        End Using
    End Function

    Public Function DeleteModule(IdModule As Integer, companyCode As String) As Boolean Implements IModuleRepository.DeleteModule
        Dim Result As Boolean
        Dim query As String
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Try
                If conx.sqlWebConection.State = ConnectionState.Closed Then
                    conx.sqlWebConection.Open()
                End If

                conx.InTransaction = True
                conx.IndigoTransaction = conx.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Eliminar Relacion Modulos")
                conx.AddParam("IdModule", SqlDbType.Int, IdModule)

                query = "DELETE FROM Security.ModuleForm WHERE IdModule = @IdModule"
                conx.ExecuteCommandParams(query, False)

                query = "DELETE FROM Security.ModuleTitle WHERE IdModule = @IdModule"
                conx.ExecuteCommandParams(query, False)

                query = "DELETE FROM Security.ProductModule WHERE IdModule = @IdModule"
                conx.ExecuteCommandParams(query, False)

                query = "DELETE FROM Security.Module WHERE Id = @IdModule"
                conx.ExecuteCommandParams(query, False)

                conx.IndigoTransaction.Commit()
                Result = True
            Catch ex As Exception
                conx.IndigoTransaction.Rollback()
                Result = False
            Finally
                conx.sqlWebConection.Close()
            End Try

            Return Result
        End Using
    End Function

    Public Function ChangeStateModule(IdModule As Integer, state As Byte, companyCode As String) As Boolean Implements IModuleRepository.ChangeStateModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.Module SET State = @State WHERE Id = @IdModule"

            conx.AddParam("IdModule", SqlDbType.Int, IdModule)
            conx.AddParam("State", SqlDbType.Bit, state)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function ListAllModule() As List(Of Modules) Implements IModuleRepository.ListAllModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of Modules) = New List(Of Modules)
            Dim query = String.Format("SELECT Id,Name,Description,State FROM Security.Module ORDER BY Id")
            Dim dt As DataTable = conx.ExecuteCommand_Data(query)
            Dim obj As Modules = Nothing

            For index As Integer = 0 To dt.Rows.Count() - 1
                obj = New Modules() With {
                .IdModule = CInt(dt.Rows(index)("Id")),
                .ModuleName = dt.Rows(index)("Name").ToString(),
                .Description = dt.Rows(index)("Description").ToString(),
                .State = Convert.ToByte(dt.Rows(index)("State"))
            }
                list.Add(obj)
            Next

            Return list
        End Using
    End Function

#End Region

#Region "ProductModule"
    Public Function GetProductModuleByModule(IdModule As Integer) As List(Of ProductModule) Implements IModuleRepository.GetProductModuleByModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of ProductModule) = New List(Of ProductModule)()
            Dim dt1 As DataTable = Nothing

            dt1 = conx.ExecuteCommand_Data(String.Format("SELECT Id,IdProduct,IdModule FROM Security.ProductModule WHERE  IdModule = {0}", IdModule))

            For index As Integer = 0 To dt1.Rows.Count() - 1
                Dim vieModule = New ProductModule() With {
                .Id = CInt(dt1.Rows(index)("IdModule")),
                .IdProduct = CInt(dt1.Rows(index)("IdProduct")),
                .IdModule = CInt(dt1.Rows(index)("IdModule"))
            }

                list.Add(vieModule)
            Next

            Return list
        End Using
    End Function

    Public Function UpdateProductModule(modules As Modules) As Boolean Implements IModuleRepository.UpdateProductModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.ProductModule SET  IdProduct = @IdProduct WHERE IdModule = @IdModule"
            conx.AddParam("IdProduct", SqlDbType.Int, modules.ProductCatalog.IdProduct)
            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function SaveProductModule(modules As Modules) As Boolean Implements IModuleRepository.SaveProductModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO Security.ProductModule(IdProduct,IdModule) VALUES(@IdProduct,@IdModule)"
            conx.AddParam("IdProduct", SqlDbType.Int, modules.ProductCatalog.IdProduct)
            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

#End Region

#Region "ModuleTitle"
    Public Function ListModuleTitleByModule(IdModule As Integer) As List(Of ModuleTitle) Implements IModuleRepository.ListModuleTitleByModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of ModuleTitle) = New List(Of ModuleTitle)
            Dim query = String.Format("SELECT A.Id,A.IdModule,A.IdTitle,A.[Order] ,B.Name,B.State
					                FROM Security.ModuleTitle A
					                INNER JOIN Security.Title B ON A.IdTitle = B.Id
					                WHERE A.IdModule = {0} ORDER BY A.IdTitle, A.[Order] ASC", IdModule)
            Dim dt As DataTable = conx.ExecuteCommand_Data(query)

            Dim _title As Title = Nothing
            Dim _moduletitle As ModuleTitle = Nothing

            For index As Integer = 0 To dt.Rows.Count() - 1
                _title = New Title() With {
                .IdTitle = CInt(dt.Rows(index)("IdTitle")),
                .TitleName = dt.Rows(index)("Name").ToString(),
                .TitleOrder = CInt(dt.Rows(index)("Order")),
                .State = Convert.ToByte(dt.Rows(index)("State"))
            }

                _moduletitle = New ModuleTitle() With {
                .Id = CInt(dt.Rows(index)("Id")),
                .IdModule = CInt(dt.Rows(index)("IdModule")),
                .IdTitle = CInt(dt.Rows(index)("IdTitle")),
                .Order = CInt(dt.Rows(index)("Order")),
                .Title = _title
            }

                list.Add(_moduletitle)
            Next

            Return list
        End Using
    End Function

    Public Function ListAllModuleTitleByModule(IdModule As Integer) As List(Of ModuleTitle) Implements IModuleRepository.ListAllModuleTitleByModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of ModuleTitle) = New List(Of ModuleTitle)()
            Dim dt1 As DataTable = Nothing

            dt1 = conx.ExecuteCommand_Data(String.Format("SELECT Id,IdModule,IdTitle, [Order] FROM Security.ModuleTitle WHERE  IdModule = {0}", IdModule))

            For index As Integer = 0 To dt1.Rows.Count() - 1
                Dim vieModule = New ModuleTitle() With {
                .Id = CInt(dt1.Rows(index)("IdModule")),
                .IdTitle = CInt(dt1.Rows(index)("IdTitle")),
                .IdModule = CInt(dt1.Rows(index)("IdModule")),
                .Order = CInt(dt1.Rows(index)("Order"))
            }

                list.Add(vieModule)
            Next

            Return list
        End Using
    End Function

    Public Function DeleteModuleTitle(modules As ModuleTitle) As Boolean Implements IModuleRepository.DeleteModuleTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "DELETE FROM Security.ModuleTitle WHERE IdModule = @IdModule and IdTitle = @IdTitle"
            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("IdTitle", SqlDbType.Int, modules.IdTitle)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function SaveModuleTitle(modules As ModuleTitle) As Boolean Implements IModuleRepository.SaveModuleTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO Security.ModuleTitle(IdModule,IdTitle,[Order]) VALUES(@IdModule,@IdTitle,@Order)"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("IdTitle", SqlDbType.Int, modules.IdTitle)
            conx.AddParam("Order", SqlDbType.Int, modules.Order)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function UpdateModuleTitle(modules As ModuleTitle) As Boolean Implements IModuleRepository.UpdateModuleTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.ModuleTitle SET [Order] = @Order WHERE IdModule = @IdModule and IdTitle = @IdTitle"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("IdTitle", SqlDbType.Int, modules.IdTitle)
            conx.AddParam("Order", SqlDbType.Int, modules.Order)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

#End Region

#Region "ModuleForm"

    Public Function ListModuleFormByModule(IdModule As Integer) As List(Of ModuleForm) Implements IModuleRepository.ListModuleFormByModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of ModuleForm) = New List(Of ModuleForm)
            Dim query = String.Format("SELECT A.Id, A.IdModule,A.IdTitle,A.IdForm,A.FormOrder,
	                                    B.Name, B.PrintEvents, B.ClassName, B.AssemblyName, B.State
                                    FROM Security.ModuleForm A
                                    INNER JOIN Security.Form B ON A.IdForm = B.Id
                                    WHERE A.IdModule = {0} ORDER BY A.IdTitle,A.FormOrder ASC", IdModule)
            Dim dt As DataTable = conx.ExecuteCommand_Data(query)

            Dim _moduleform As ModuleForm = Nothing
            Dim _form As VieDBForm = Nothing

            For index As Integer = 0 To dt.Rows.Count() - 1
                _form = New VieDBForm() With {
                .IdForm = CInt(dt.Rows(index)("IdForm")),
                .FormName = dt.Rows(index)("Name").ToString(),
                .FormOrder = CInt(dt.Rows(index)("FormOrder")),
                .ClassName = dt.Rows(index)("ClassName").ToString(),
                .AssemblyName = dt.Rows(index)("AssemblyName").ToString(),
                .State = Convert.ToByte(dt.Rows(index)("State"))
            }


                _moduleform = New ModuleForm() With {
                .Id = CInt(dt.Rows(index)("Id")),
                .IdModule = CInt(dt.Rows(index)("IdModule")),
                .IdTitle = CInt(dt.Rows(index)("IdTitle")),
                .IdForm = CInt(dt.Rows(index)("IdForm")),
                .FormOrder = CInt(dt.Rows(index)("FormOrder")),
                .Form = _form
            }

                list.Add(_moduleform)
            Next

            Return list
        End Using
    End Function

    Public Function ListAllModuleFormByModule(IdModule As Integer) As List(Of ModuleForm) Implements IModuleRepository.ListAllModuleFormByModule
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of ModuleForm) = New List(Of ModuleForm)()
            Dim dt1 As DataTable = Nothing

            dt1 = conx.ExecuteCommand_Data(String.Format("SELECT Id,IdModule,IdTitle,IdForm,FormOrder FROM Security.ModuleForm WHERE IdModule = {0}", IdModule))

            For index As Integer = 0 To dt1.Rows.Count() - 1
                Dim vieModule = New ModuleForm() With {
                .Id = CInt(dt1.Rows(index)("IdModule")),
                .IdTitle = CInt(dt1.Rows(index)("IdTitle")),
                .IdModule = CInt(dt1.Rows(index)("IdModule")),
                .IdForm = CInt(dt1.Rows(index)("IdForm")),
                .FormOrder = CInt(dt1.Rows(index)("FormOrder"))
            }

                list.Add(vieModule)
            Next

            Return list
        End Using
    End Function

    Public Function DeleteModuleForm(modules As ModuleForm) As Boolean Implements IModuleRepository.DeleteModuleForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "DELETE FROM Security.ModuleForm WHERE IdModule = @IdModule and IdTitle = @IdTitle and IdForm = @IdForm"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("IdTitle", SqlDbType.Int, modules.IdTitle)
            conx.AddParam("IdForm", SqlDbType.Int, modules.IdForm)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function SaveModuleForm(modules As ModuleForm) As Boolean Implements IModuleRepository.SaveModuleForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO Security.ModuleForm(IdModule,IdTitle,IdForm,FormOrder) VALUES(@IdModule,@IdTitle,@IdForm,@FormOrder)"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("IdTitle", SqlDbType.Int, modules.IdTitle)
            conx.AddParam("IdForm", SqlDbType.Int, modules.IdForm)
            conx.AddParam("FormOrder", SqlDbType.Int, modules.FormOrder)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function UpdateModuleForm(modules As ModuleForm) As Boolean Implements IModuleRepository.UpdateModuleForm
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.ModuleForm SET FormOrder = @FormOrder WHERE IdModule = @IdModule and IdTitle = @IdTitle and IdForm = @IdForm"

            conx.AddParam("IdModule", SqlDbType.Int, modules.IdModule)
            conx.AddParam("IdTitle", SqlDbType.Int, modules.IdTitle)
            conx.AddParam("IdForm", SqlDbType.Int, modules.IdForm)
            conx.AddParam("FormOrder", SqlDbType.Int, modules.FormOrder)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

#End Region

End Class
