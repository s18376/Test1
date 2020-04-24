using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    [Route("api/task/1")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        // GET: api/TeamMember
        [HttpGet("{IdTeamMember}")]
        public IActionResult GetTeamMember(string IdTeamMember)
        {
            var member = new TeamMember();
            var tasksByMember = new List<Models.Task>();
            var tasksToMember = new List<Models.Task>();
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18376;Integrated Security=True"))
            {
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Select IdTeamMember, FirstName, LastName, Email from TeamMember Where IdTeamMember=@IdTeamMember;";
                   
                    con.Open();
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return BadRequest("Specified Team Member does not exist");
                    }
                    
                    while (dr.Read())
                    {
                        member.IdTeamMember = (int)dr["IdTeamMember"];
                        member.FirstName = dr["FirstName"].ToString();
                        member.LastName = dr["LastName"].ToString();
                        member.Email = dr["Email"].ToString();
                     }
                }
                
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Select IdTask, Name, Description, Deadline, IdProject, IdTaskType, IdAssignedTo, IdCreatorIdCreator from Task Where IdCreator=@IdTeamMember Order By IdProject desc;";
                   
                    con.Open();
                    var dr = com.ExecuteReader();
                    var task = new Models.Task();
                    while (dr.Read())
                    {
                        task.IdTask = (int)dr["IdTask"];
                        task.Name = (int)dr["Name"];
                        task.Description = dr["Description"].ToString();
                        task.Deadline = (DateTime)dr["Deadline"];
                        task.Project = new Project
                        {
                            IdProject = (int)dr["IdProject"]

                        };
                        task.TaskType = new TaskType
                        {
                            IdTaskType = (int)dr["IdTaskType"]
                        };
                        task.AssignedTo = new TeamMember
                        {
                            IdTeamMember = (int)dr["IdAssignedTo"]
                        };
                        task.Creator = new TeamMember
                        {
                            IdTeamMember = (int)dr["IdTeamMember"]
                        };
                        tasksByMember.Add(task);
                     }
                }
                
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Select IdTask, Name, Description, Deadline, IdProject, IdTaskType, IdAssignedTo, IdCreator from Task Where IdAssignedTo=@IdTeamMember Order By IdProject desc;";
                   
                    con.Open();
                    var dr = com.ExecuteReader();
                    var task = new Models.Task();
                    while (dr.Read())
                    {
                        task.IdTask = (int)dr["IdTask"];
                        task.Name = (int)dr["Name"];
                        task.Description = dr["Description"].ToString();
                        task.Deadline = (DateTime)dr["Deadline"];
                        task.Project = new Project
                        {
                            IdProject = (int)dr["IdProject"]

                        };
                        task.TaskType = new TaskType
                        {
                            IdTaskType = (int)dr["IdTaskType"]
                        };
                        task.AssignedTo = new TeamMember
                        {
                            IdTeamMember = (int)dr["IdAssignedTo"]
                        };
                        task.Creator = new TeamMember
                        {
                            IdTeamMember = (int)dr["IdTeamMember"]
                        };
                        tasksByMember.Add(task);
                     }
                }
            }
            return Ok(member + "\n" + tasksByMember + "\n" + tasksToMember);

        }
        
    }
}
