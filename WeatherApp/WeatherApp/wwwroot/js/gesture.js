function initializeGesture(element, controller, action, previousDate, nextDate) {
    console.log('initializeGesture: controller=' + controller + ', action=' + action + ', previousDate=' + previousDate + ', action=' + nextDate);

    const swipeLeftUrl = getSwipeUrl(controller, action, nextDate);
    console.log('swipeLeftUrl=' + swipeLeftUrl);

    const swipeRightUrl = getSwipeUrl(controller, action, previousDate);
    console.log('swipeRightUrl=' + swipeRightUrl);

    const pinchInUrl = getPinchUrl(controller, getPinchInAction(action));
    console.log('pinchInUrl=' + pinchInUrl);

    const pinchOutUrl = getPinchUrl(controller, getPinchOutAction(action));
    console.log('pinchOutUrl=' + pinchOutUrl);

    startGesture(element, swipeLeftUrl, swipeRightUrl, pinchInUrl, pinchOutUrl);
}

function getSwipeUrl(controller, action, date) {
    let url = '/' + controller + '/' + action + "?date=" + date;

    const culture = getParameterByName('culture');
    if (culture) {
        url = url + '&culture=' + culture;
    }
    return url
}

function getPinchUrl(controller, nextAction) {
    console.log('getPinchUrl: nextAction=' + nextAction);

    if (!nextAction) {
        return null;
    }

    const date = getParameterByName('date');
    const culture = getParameterByName('culture');

    let url = '/' + controller + '/' + nextAction;

    if (date || culture) {
        url = url + '?';
    }

    if (date) {
        url = url + 'date=' + date;
    }

    if (date && culture) {
        url = url + '&';
    }

    if (culture) {
        url = url + 'culture=' + culture;
    }

    return url;
}

function getPinchInAction(action) {
    switch (action) {
        case 'index':
            return 'week';
        case 'week':
            return 'month';
        case 'month':
            return 'year';
        default:
            return null;
    }
}

function getPinchOutAction(action) {
    switch (action) {
        case 'year':
            return 'month';
        case 'month':
            return 'week';
        case 'week':
            return 'index';
        default:
            return null;
    }
}

function startGesture(element, swipeLeftUrl, swipeRightUrl, pinchInUrl, pinchOutUrl) {
    const manager = new Hammer.Manager(element);

    startSwipe(manager, swipeLeftUrl, swipeRightUrl);
    startPinch(manager, pinchInUrl, pinchOutUrl);
}

function startSwipe(manager, swipeLeftUrl, swipeRightUrl) {
    manager.add(new Hammer.Swipe());

    if (swipeLeftUrl) {
        manager.on('swipeleft', () => window.location.href = swipeLeftUrl);
    }

    if (swipeRightUrl) {
        manager.on('swiperight', () => window.location.href = swipeRightUrl);
    }
}

function startPinch(manager, pinchInUrl, pinchOutUrl) {
    manager.add(new Hammer.Pinch());

    if (pinchInUrl) {
        manager.on('pinchin', () => window.location.href = pinchInUrl);
    }

    if (pinchOutUrl) {
        manager.on('pinchout', () => window.location.href = pinchOutUrl);
    }
}

