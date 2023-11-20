using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class SubjectController : Controller
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var subjects = await _subjectService.GetSubjects();

        return View(subjects);
    }
    
    [HttpGet]
    public IActionResult AddSubject()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSubject(Subject subject)
    {
        if (ModelState.IsValid)
        {
            var addingResult = await _subjectService.AddSubject(subject);
        
            if (!addingResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = addingResult.Message;
                
                return View(subject);
            }
            
            return RedirectToAction("Index"); 
        }
        
        return View(subject);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var deletingResult = await _subjectService.DeleteSubject(id);
        
        if (!deletingResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = deletingResult.Message;
        }
        
        return RedirectToAction("Index"); 
    }
    
    [HttpGet]
    public async Task<IActionResult> EditValuesOfSubject(int id)
    { 
        var subjectToEdit = await _subjectService.GetClassById(id);
        
        return View(subjectToEdit);
    }

    [HttpPost]
    public async Task<IActionResult> EditValuesOfSubject(Subject updatedSubject)
    {
        if (ModelState.IsValid)
        {
            var updatingResult = await _subjectService.UpdateSubject(updatedSubject);
        
            if (!updatingResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = updatingResult.Message;
            }
            
            return RedirectToAction("Index");
        }

        return View(updatedSubject);
    }
}