REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 2.1.0.2569

REM  Dump @2017/10/24 10:46:32


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes

Namespace TMIC.FooDB

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `pathways`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `pathways` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `smpdb_id` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
'''   `kegg_map_id` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
'''   `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
'''   `created_at` datetime DEFAULT NULL,
'''   `updated_at` datetime DEFAULT NULL,
'''   PRIMARY KEY (`id`)
''' ) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("pathways", Database:="foodb", SchemaSQL:="
CREATE TABLE `pathways` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `smpdb_id` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `kegg_map_id` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;")>
Public Class pathways: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="id"), XmlAttribute> Public Property id As Long
    <DatabaseField("smpdb_id"), DataType(MySqlDbType.VarChar, "255"), Column(Name:="smpdb_id")> Public Property smpdb_id As String
    <DatabaseField("kegg_map_id"), DataType(MySqlDbType.VarChar, "255"), Column(Name:="kegg_map_id")> Public Property kegg_map_id As String
    <DatabaseField("name"), DataType(MySqlDbType.VarChar, "255"), Column(Name:="name")> Public Property name As String
    <DatabaseField("created_at"), DataType(MySqlDbType.DateTime), Column(Name:="created_at")> Public Property created_at As Date
    <DatabaseField("updated_at"), DataType(MySqlDbType.DateTime), Column(Name:="updated_at")> Public Property updated_at As Date
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `pathways` (`id`, `smpdb_id`, `kegg_map_id`, `name`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `pathways` (`id`, `smpdb_id`, `kegg_map_id`, `name`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `pathways` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `pathways` SET `id`='{0}', `smpdb_id`='{1}', `kegg_map_id`='{2}', `name`='{3}', `created_at`='{4}', `updated_at`='{5}' WHERE `id` = '{6}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `pathways` WHERE `id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `pathways` (`id`, `smpdb_id`, `kegg_map_id`, `name`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, id, smpdb_id, kegg_map_id, name, DataType.ToMySqlDateTimeString(created_at), DataType.ToMySqlDateTimeString(updated_at))
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{id}', '{smpdb_id}', '{kegg_map_id}', '{name}', '{created_at}', '{updated_at}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `pathways` (`id`, `smpdb_id`, `kegg_map_id`, `name`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, id, smpdb_id, kegg_map_id, name, DataType.ToMySqlDateTimeString(created_at), DataType.ToMySqlDateTimeString(updated_at))
    End Function
''' <summary>
''' ```SQL
''' UPDATE `pathways` SET `id`='{0}', `smpdb_id`='{1}', `kegg_map_id`='{2}', `name`='{3}', `created_at`='{4}', `updated_at`='{5}' WHERE `id` = '{6}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, smpdb_id, kegg_map_id, name, DataType.ToMySqlDateTimeString(created_at), DataType.ToMySqlDateTimeString(updated_at), id)
    End Function
#End Region
Public Function Clone() As pathways
                  Return DirectCast(MyClass.MemberwiseClone, pathways)
              End Function
End Class


End Namespace
