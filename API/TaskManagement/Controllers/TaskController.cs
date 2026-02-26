using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model;
using TaskManagement.DatabaseContext;
using Dapper;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly DapperContext _context;
        public TaskController(DapperContext context) => _context = context;
        [HttpGet]
    public async Task<IActionResult> GetTasks(string search = "")
        {
            var query = "SELECT * FROM Tasks WHERE TaskTitle LIKE @search OR TaskStatus LIKE @search";
            using (var connection = _context.CreateConnection())
            {
                var tasks = await connection.QueryAsync<TaskItem>(query, new { search = $"%{search}%" });
                return Ok(tasks);
            }
        }
        [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody]TaskItem task)
        {
            var query = @"INSERT INTO Tasks (TaskTitle, TaskDescription, TaskDueDate, TaskStatus, TaskRemarks, CreatedOn, CreatedByName)
                      VALUES (@TaskTitle, @TaskDescription, @TaskDueDate, @TaskStatus, @TaskRemarks, GETDATE(), @CreatedByName)";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, task);
                return Ok(new { message = "Task Created Successfully" });
            }
        }
        [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        {
            var query = @"UPDATE Tasks SET
                      TaskTitle = @TaskTitle, TaskDescription = @TaskDescription,
                      TaskDueDate = @TaskDueDate, TaskStatus = @TaskStatus,
                      TaskRemarks = @TaskRemarks, LastUpdatedOn = GETDATE(),
                      LastUpdatedByName = @LastUpdatedByName
                      WHERE TaskId = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    task.TaskTitle,
                    task.TaskDescription,
                    task.TaskDueDate,
                    task.TaskStatus,
                    task.TaskRemarks,
                    task.LastUpdatedByName,
                    id
                });
                return Ok(new { message = "Task Updated Successfully" });
            }
        }
        [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
        {
            var query = "DELETE FROM Tasks WHERE TaskId = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
                return Ok(new { message = "Task Deleted Successfully" });
            }
        }
    }
}
