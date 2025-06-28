/*
    작성자: 박용준 (https://dul.me/about)
    타이틀: 닷넷코리아 - 첨부 파일 관리자 ADO.NET 리포지토리 클래스
    설명: 순수 ADO.NET을 사용하여 AttachFiles 테이블과 연동하는 리포지토리 구현체입니다.
    작성일: 2017-12-08
    수정일:
        - 2021-11-12: .NET Standard 프로젝트로 이동
        - 2025-06-28: Azunt 네임스페이스 및 ADO.NET 리팩터링
*/
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Azunt.AttachFileManagement
{
    public class AttachFileRepository : IAttachFileRepository
    {
        private readonly string _connectionString;

        public AttachFileRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"]?.ConnectionString
                                ?? throw new ConfigurationErrorsException("ConnectionString 설정이 필요합니다.");
        }

        public AttachFileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(AttachFileModel model)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesAdd", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@BoardId", model.BoardId);
                cmd.Parameters.AddWithValue("@ArticleId", model.ArticleId);
                cmd.Parameters.AddWithValue("@FileName", model.FileName);
                cmd.Parameters.AddWithValue("@FileSize", model.FileSize);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<AttachFileModel> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = new List<AttachFileModel>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesGetAll", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadModel(reader));
                    }
                }
            }

            return result;
        }

        public List<AttachFileModel> GetByBoardAndArticle(int boardId, int articleId)
        {
            var result = new List<AttachFileModel>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesGetByBoardAndArticle", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BoardId", boardId);
                cmd.Parameters.AddWithValue("@ArticleId", articleId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadModel(reader));
                    }
                }
            }

            return result;
        }

        public List<AttachFileModel> GetByFileName(string fileName)
        {
            var result = new List<AttachFileModel>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesGetByFileName", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileName", fileName);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadModel(reader));
                    }
                }
            }

            return result;
        }

        public AttachFileModel GetById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesGetById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadModel(reader);
                    }
                }
            }

            return null;
        }

        public void RemoveById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesRemoveById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateById(string fileName, int fileSize, int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesUpdateById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FileName", fileName);
                cmd.Parameters.AddWithValue("@FileSize", fileSize);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDownCountById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AttachFilesUpdateDownCountById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static AttachFileModel ReadModel(SqlDataReader reader)
        {
            return new AttachFileModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                BoardId = reader.GetInt32(reader.GetOrdinal("BoardId")),
                ArticleId = reader.GetInt32(reader.GetOrdinal("ArticleId")),
                FileName = reader.GetString(reader.GetOrdinal("FileName")),
                FileSize = reader.GetInt32(reader.GetOrdinal("FileSize")),
                DownCount = reader.GetInt32(reader.GetOrdinal("DownCount")),
                TimeStamp = reader.GetDateTimeOffset(reader.GetOrdinal("TimeStamp")) // 이 필드가 없으면 DateTime으로 대체 가능
            };
        }
    }
}
