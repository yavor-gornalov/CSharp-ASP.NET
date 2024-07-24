using Homies.Contracts;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homies.Controllers
{
	public class EventController : BaseController
	{
		private readonly IEventService eventService;

		public EventController(IEventService _eventService)
		{
			eventService = _eventService;
		}

		public async Task<IActionResult> All()
		{
			var allEvents = await eventService.GetAllEventsAsync();

			return View(allEvents);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new AddEventViewModel();

			model.Types = await eventService.GetAllEventTypesAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddEventViewModel model)
		{
			var organiserId = GetUserId();
			if (organiserId == null)
			{
				return Unauthorized();
			}

			if (!ModelState.IsValid)
			{
				model.Types = await eventService.GetAllEventTypesAsync();
				return View(model);
			}

			try
			{
				await eventService.AddEventAsync(model, organiserId);
				return RedirectToAction(nameof(All));
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			}
		}

		public async Task<IActionResult> Join(int id)
		{
			try
			{
				var participantId = GetUserId();
				await eventService.AddEventToUserCollectionAsync(id, participantId);

				return RedirectToAction(nameof(Joined));
			}
			catch (ArgumentException)
			{
				return RedirectToAction(nameof(All));
			}
		}

		public async Task<IActionResult> Joined()
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return Unauthorized();
			}

			var userEvents = await eventService.GetUserEventsAsync(userId);

			return View(userEvents);
		}

		public async Task<IActionResult> Leave(int id)
		{

			try
			{
				var userId = GetUserId();
				await eventService.RemoveEventFromUserCollectionAsync(id, userId);
				return RedirectToAction(nameof(All));
			}
			catch (ArgumentException)
			{
				return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await eventService.GetEventByIdAsync(id);

			if (model == null)
			{
				return RedirectToAction(nameof(All));
			}

			model.Types = await eventService.GetAllEventTypesAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AddEventViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userId = GetUserId();
			if (userId == null)
			{
				return Unauthorized();
			}

			try
			{
				await eventService.EditEventAsync(model, id, userId);
			}
			catch
			{

			}
			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var currentEvent = await eventService.GetEventDetailsAsync(id);

			if (currentEvent == null)
			{
				return RedirectToAction(nameof(All));
			}

			return View(currentEvent);
		}
	}
}
